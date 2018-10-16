using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gap.Insurance.Web.Infrastructure;
using Gap.Insurance.Web.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Gap.Insurance.Web.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;

        public InsuranceService(IOptions<AppSettings> settings, HttpClient apiClient)
        {
            _settings = settings;
            _apiClient = apiClient;
        }

        public Task<ViewModels.Insurance> GetInsuranceAsync(int insuranceId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<ViewModels.Insurance>> GetInsurancesAsync()
        {
            var uri = API.Insurance.GetAllInsurance(_settings.Value.InsuranceUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new List<ViewModels.Insurance>();

            return JsonConvert.DeserializeObject<List<ViewModels.Insurance>>(await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteAsync(int id)
        {
            var uri = API.Insurance.MainUri(_settings.Value.InsuranceUrl);
            var data = new { insuranceId = id };
            var request = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
            };

            var response = await _apiClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                response.EnsureDomainException();
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(ViewModels.Insurance insurance)
        {
            var uri = API.Insurance.MainUri(_settings.Value.InsuranceUrl);
            var orderContent = new StringContent(JsonConvert.SerializeObject(insurance), Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, orderContent);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                response.EnsureDomainException();
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
