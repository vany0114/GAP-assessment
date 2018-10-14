using FluentValidation;
using Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Validations
{
    public class DeleteInsuranceRequestValidator : AbstractValidator<DeleteInsuranceRequest>
    {
        public DeleteInsuranceRequestValidator()
        {
            RuleFor(request => request.InsuranceId).NotEmpty().WithMessage("Insurance Id is required.");
        }
    }
}
