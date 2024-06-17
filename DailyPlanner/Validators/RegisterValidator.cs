namespace DailyPlanner.Validators
{
    using DailyPlanner.Models;

    using FluentValidation;

    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator() 
        {
            RuleFor(_ => _.UserName).NotNull().NotEmpty();
            RuleFor(_ => _.UserPassword).NotNull().NotEmpty();
            RuleFor(_ => _.Email).NotNull().NotEmpty();
            RuleFor(_ => _.PhoneNumber).NotNull().NotEmpty();
            RuleFor(_ => _.FirstName).NotNull().NotEmpty();
            RuleFor(_ => _.LastName).NotNull().NotEmpty();
            RuleFor(_ => _.Email).EmailAddress();
        }
    }
}
