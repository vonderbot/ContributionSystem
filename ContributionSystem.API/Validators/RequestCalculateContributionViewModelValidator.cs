using ContributionSystem.ViewModels.Models.Contribution;
using FluentValidation;

namespace ContributionSystem.ViewModels.Validators
{
    public class RequestCalculateContributionViewModelValidator : AbstractValidator<RequestCalculateContributionViewModel>
    {
        public RequestCalculateContributionViewModelValidator()
        {
            RuleFor(t => t.StartValue)
                .NotEmpty()
                .WithMessage("StartValue is Empty")
                .GreaterThan(0)
                .WithMessage("StartValue can`t be less then 0.01")
                .ScalePrecision(2, 12)
                .WithMessage("StartValue can only have 12 numbers, 2 of them after the decimal point");

            RuleFor(t => t.Term)
                .NotEmpty()
                .WithMessage("Term is Empty")
                .GreaterThan(0)
                .WithMessage("Term can`t be less then 1");

            RuleFor(t => t.Percent)
                .NotEmpty()
                .WithMessage("Percent is Empty")
                .GreaterThan(0)
                .WithMessage("Percent can`t be less then 0.01")
                .ScalePrecision(2, 6)
                .WithMessage("Percent can only have 6 numbers, 2 of them after the decimal point");
        }
    }
}
