using Gap.Insurance.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Gap.Insurance.Web.Infrastructure.Exceptions;

namespace Gap.Insurance.Web.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<Exception> EnsureDomainException(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var error = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(error))
                    return new Exception(response.StatusCode.ToString());

                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(error);
                return new DomainException(errorMessage?.messages[0] ?? response.StatusCode.ToString());
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new DomainException("You must re-login again.");

            return null;
        }
    }
}
