namespace WebApplication1.Validators
{
    using FluentValidation;

    using WebApplication1.DataModels;

    public class RegisterValidator : AbstractValidator<RegisterDataModel>
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
