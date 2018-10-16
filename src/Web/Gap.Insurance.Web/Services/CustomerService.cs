using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gap.Insurance.Web.Infrastructure;
using Gap.Insurance.Web.Infrastructure.Extensions;
using Gap.Insurance.Web.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Gap.Insurance.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;

        public CustomerService(IOptions<AppSettings> settings, HttpClient apiClient)
        {
            _settings = settings;
            _apiClient = apiClient;
        }

        public async Task<IList<Customer>> GetCustomersAsync()
        {
            var uri = API.Customer.MainUri(_settings.Value.InsuranceUrl);
            var response = await _apiClient.GetAsync(uri);
            await EnsureDomainException(response);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new List<Customer>();

            return JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            var uri = API.Customer.GetCustomerById(_settings.Value.InsuranceUrl, customerId);
            var response = await _apiClient.GetAsync(uri);
            await EnsureDomainException(response);

            return response.StatusCode == HttpStatusCode.NotFound ?
                null :
                JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

        public async Task AssignInsuranceAsync(Assignment assignment)
        {
            var uri = API.Customer.MainUri(_settings.Value.InsuranceUrl);
            var data = new { insuranceId = assignment.InsuranceId, customerId = assignment.CustomerId };
            var orderContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await _apiClient.PutAsync(uri, orderContent);
            await EnsureDomainException(response);

            response.EnsureSuccessStatusCode();
        }

        public async Task CancelInsuranceAsync(Assignment assignment)
        {
            var uri = API.Customer.MainUri(_settings.Value.InsuranceUrl);
            var data = new { insuranceId = assignment.InsuranceId, customerId = assignment.CustomerId };
            var request = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
            };

            var response = await _apiClient.SendAsync(request);
            await EnsureDomainException(response);

            response.EnsureSuccessStatusCode();
        }

        private async Task EnsureDomainException(HttpResponseMessage response)
        {
            var ex = await response.EnsureDomainException();
            if (ex != null)
            {
                throw ex;
            }
        }
    }
}
