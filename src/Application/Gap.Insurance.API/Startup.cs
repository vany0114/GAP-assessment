using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Gap.Domain.Customer.Persistence;
using Gap.Domain.Customer.Repository;
using Gap.Domain.Insurance.Persistence;
using Gap.Domain.Insurance.Repository;
using Gap.Insurance.API.Infrastructure.AutofacModules;
using Gap.Insurance.API.Infrastructure.Extensions;
using Gap.Insurance.API.Infrastructure.Filters;
using Gap.Insurance.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gap.Insurance.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                    options.Filters.Add(typeof(ValidatorActionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // swagger configuration
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Gap.Insurance HTTP API",
                    Version = "v1",
                    Description = "The GAP Insurance Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["ConnectionString"],
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(CustomerContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            services.AddDbContext<InsuranceContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["ConnectionString"],
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(InsuranceContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            services.RegisterLazyTypes();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IInsuranceRepository, InsuranceRepository>();
            services.AddTransient<IInsuranceService, InsuranceService>();
            services.AddTransient<ICustomerService, CustomerService>();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseMvc();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gap.Insurance V1");
                    c.RoutePrefix = string.Empty;
                });
        }
    }
}
