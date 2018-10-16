using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gap.Insurance.Web.ViewModels
{
    public class Insurance : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int CoveragePeriod { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Cost { get; set; }

        [Required]
        public RiskType Risk { get; set; }

        public IList<InsuranceCoverage> Coverages { get; set; }

        public List<SelectListItem> Risks { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = RiskType.High.ToString(), Text = RiskType.High.ToString() },
            new SelectListItem { Value = RiskType.Low.ToString(), Text = RiskType.Low.ToString() },
            new SelectListItem { Value = RiskType.Medium.ToString(), Text = RiskType.Medium.ToString()  },
            new SelectListItem { Value = RiskType.MediumHigh.ToString(), Text = RiskType.MediumHigh.ToString()  },
        };

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate < DateTime.UtcNow)
                yield return new ValidationResult("Invalid start date.", new[] { "StartDate" });

            if (CoveragePeriod == default(int))
                yield return new ValidationResult("Invalid coverage period.", new[] { "CoveragePeriod" });

            if (Cost <= 0)
                yield return new ValidationResult("Invalid insurance cost.", new[] { "Cost" });
        }
    }

    public class InsuranceCoverage
    {
        public CoverageType Coverage { get; set; }

        public decimal Percentage { get; set; }
    }

    public class CoverageType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public enum RiskType
    {
        Low = 1,
        Medium = 2,
        MediumHigh = 3,
        High = 4
    }
}
