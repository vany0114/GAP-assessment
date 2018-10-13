using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gap.Domain.Customer.Persistence
{
    public class CustomerContextSeed
    {
        public async Task SeedAsync(CustomerContext context, ILogger<CustomerContextSeed> logger)
        {
            try
            {
                using (context)
                {
                    if (!context.Customers.Any())
                    {
                        context.Customers.AddRange(GetPreconfiguredCustomers());
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, $"Exception {ex.GetType().Name} with message ${ex.Message}");
                throw;
            }
        }

        private IEnumerable<Model.Customer> GetPreconfiguredCustomers() => new List<Model.Customer>
        {
            new Model.Customer("James Hetfield", "papajames@metallica.com", null),
            new Model.Customer("Rob Haldford", "themaniac@judaspriest.com", null),
            new Model.Customer("Steve Vai", "steve@latinmail.com", null),
            new Model.Customer("Joe Satriani", "joesatriani@yahoo.com", "3164569563")
        };
    }
}
