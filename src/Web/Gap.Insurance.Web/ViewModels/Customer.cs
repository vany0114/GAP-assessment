using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gap.Insurance.Web.ViewModels
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

    public class Assignment
    {
        public Customer Customer { get; set; }

        public List<SelectListItem> Insurances { get; set; }

        [Required]
        public int InsuranceId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
