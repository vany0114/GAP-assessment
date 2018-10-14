using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gap.Insurance.API.Infrastructure.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void RegisterLazyTypes(this IServiceCollection services)
        {
            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
        }
    }

    internal class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(provider.GetRequiredService<T>)
        {
        }
    }
}
