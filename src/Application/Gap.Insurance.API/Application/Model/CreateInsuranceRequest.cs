using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.Insurance.API.Application.Model
{
    public class CreateInsuranceRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public int CoveragePeriod { get; set; }

        public double Cost { get; set; }

        public RiskType Risk { get; set; }
    }
}
