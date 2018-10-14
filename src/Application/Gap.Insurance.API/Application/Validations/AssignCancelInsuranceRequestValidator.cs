using FluentValidation;
using Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Validations
{
    public class AssignCancelInsuranceRequestValidator : AbstractValidator<AssignCancelInsuranceRequest>
    {
        public AssignCancelInsuranceRequestValidator()
        {
            RuleFor(request => request.InsuranceId).NotEmpty().WithMessage("Insurance Id is required.");
            RuleFor(request => request.CustomerId).NotEmpty().WithMessage("Customer Id is required.");
        }
    }
}
