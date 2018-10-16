using Gap.Insurance.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Gap.Insurance.Web.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        public static async void EnsureDomainException(this HttpResponseMessage response)
        {
            var error = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(error))
                throw new Exception(response.StatusCode.ToString());

            var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(error);
            throw new Exception(errorMessage?.messages[0] ?? response.StatusCode.ToString());
        }
    }
}
