using AutoMapper;
using Gap.Domain.Insurance.Model;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Mapping
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<Domain.Insurance.Model.Insurance, ViewModel.Insurance>();
            CreateMap<InsuranceCoverage, ViewModel.InsuranceCoverage>();
            CreateMap<CoverageType, ViewModel.CoverageType>();
            CreateMap<RiskType, ViewModel.RiskType>();

            CreateMap<ViewModel.Insurance, Domain.Insurance.Model.Insurance>()
                .ConstructUsing(x => new Domain.Insurance.Model.Insurance(
                    x.Name,
                    x.Description,
                    x.StartDate,
                    x.CoveragePeriod,
                    x.Cost,
                    (RiskType) (int) x.Risk
                ));

            CreateMap<ViewModel.CreateInsuranceRequest, ViewModel.Insurance>();
        }
    }
}