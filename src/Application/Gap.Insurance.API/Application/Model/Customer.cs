namespace Gap.Insurance.API.Application.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int ActiveInsurances { get; set; }

        public int CancelledInsurances { get; set; }
    }
}
