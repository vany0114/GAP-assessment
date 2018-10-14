using FluentValidation;
using Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Validations
{
    public class CreateInsuranceRequestValidator : AbstractValidator<CreateInsuranceRequest>
    {
        public CreateInsuranceRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(request => request.StartDate).NotEmpty().WithMessage("StartDate is required.");
            RuleFor(request => request.Cost).NotEmpty().WithMessage("Cost is required.");
            RuleFor(request => request.CoveragePeriod).NotEmpty().WithMessage("CoveragePeriod is required.");
        }
    }
}
