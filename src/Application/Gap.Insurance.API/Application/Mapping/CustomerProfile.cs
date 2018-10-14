using System.Linq;
using AutoMapper;
using Gap.Domain.Customer.Model;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, ViewModel.Customer>()
                .AfterMap((domain, model) => model.ActiveInsurances = domain.Insurances.Count(x => x.Status == Status.Assigned))
                .AfterMap((domain, model) => model.CancelledInsurances = domain.Insurances.Count(x => x.Status == Status.Canceled));
        }
    }
}
