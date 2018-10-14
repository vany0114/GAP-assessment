using AutoMapper;
using Gap.Domain.Customer.Model;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, ViewModel.Customer>();
        }
    }
}
