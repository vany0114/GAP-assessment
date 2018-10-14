using FluentValidation;
using Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Application.Validations
{
    public class AddCoverageRequestValidator : AbstractValidator<AddCoverageRequest>
    {
        public AddCoverageRequestValidator()
        {
            RuleFor(request => request.InsuranceId).NotEmpty().WithMessage("Insurance Id is required.");
            RuleFor(request => request.CoverageId).NotEmpty().WithMessage("Coverage Id is required.");
            RuleFor(request => request.Percentage).NotEmpty().WithMessage("Percentage is required.");
        }
    }
}
