using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.Insurance.API.Application.Model
{
    public class AddCoverageRequest
    {
        public int InsuranceId { get; set; }

        public int CoverageId { get; set; }

        public decimal Percentage { get; set; }
    }
}
