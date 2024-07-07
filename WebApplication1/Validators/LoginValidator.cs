namespace WebApplication1.Validators
{
    using FluentValidation;

    using WebApplication1.DataModels;

    public class LoginValidator : AbstractValidator<LoginDataModel>
    {
        public LoginValidator() 
        {
            RuleFor(_ => _.UserName).NotNull().NotEmpty();
            RuleFor(_ => _.UserPassword).NotNull().NotEmpty();
        }
    }
}
