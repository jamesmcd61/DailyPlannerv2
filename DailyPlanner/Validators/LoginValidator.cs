namespace DailyPlanner.Validators
{
    using DailyPlanner.Models;
    using FluentValidation;

    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator() 
        {
            RuleFor(_ => _.UserName).NotNull().NotEmpty();
            RuleFor(_ => _.UserPassword).NotNull().NotEmpty();
        }
    }
}
