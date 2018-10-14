using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.Insurance.API.Application.Model
{
    public class AssignCancelInsuranceRequest
    {
        public int InsuranceId { get; set; }

        public int CustomerId { get; set; }
    }
}
