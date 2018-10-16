namespace Gap.Insurance.Web.Infrastructure
{
    public static class API
    {
        public static class Customer
        {
            public static string MainUri(string baseUri) => $"{baseUri}/api/v1/Customer";

            public static string GetCustomerById(string baseUri, int customerId) => $"{baseUri}/api/v1/Customer/{customerId}";
        }

        public static class Insurance
        {
            public static string MainUri(string baseUri) => $"{baseUri}/api/v1/Insurance";

            public static string GetInsuranceById(string baseUri, string insuranceId) => $"{baseUri}/api/v1/Insurance/{insuranceId}";

            public static string GetAllInsurance(string baseUri) => $"{baseUri}/api/v1/Insurance/all";
        }
    }
}
