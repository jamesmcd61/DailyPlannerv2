namespace WebApplication1.Validators
{
    using FluentValidation;

    using WebApplication1.Models;

    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator() 
        {
            RuleFor(_ => _.UserName).NotNull().NotEmpty();
            RuleFor(_ => _.UserPassword).NotNull().NotEmpty();
        }
    }
}
