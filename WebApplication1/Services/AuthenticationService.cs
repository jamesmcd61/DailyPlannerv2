namespace WebApplication1.Services
{
    using Microsoft.EntityFrameworkCore;

    using WebApplication1.Data;
    using WebApplication1.DataModels;
    using WebApplication1.Validators;

    public interface IAuthenticationService
    {
        Task<bool> CanLoginAsync(LoginDataModel credentials);

        Task<bool> HasRegisteredAsync(RegisterDataModel registrationInformation);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly PlannerContext context;
        private readonly IEncryptionService encryptionService;

        public AuthenticationService(IEncryptionService encryptionService, PlannerContext context)
        {
            this.encryptionService = encryptionService;
            this.context = context;
        }

        public async Task<bool> CanLoginAsync(LoginDataModel credentials)
        {
            LoginValidator validator = new LoginValidator();
            var result = validator.Validate(credentials);

            if (result.IsValid) {
                return await this.CheckUserExistsAsync(credentials);
            }

            return result.IsValid;
        }

        public async Task<bool> HasRegisteredAsync(RegisterDataModel registrationInformation)
        {
            // Put a DB check to add the record if one doesnt exist already.
            RegisterValidator validator = new RegisterValidator();
            var result = validator.Validate(registrationInformation);
            // Check if the user email already exists and if the User name exists already, these need to be unique.
            if (registrationInformation.UserName != string.Empty && registrationInformation.Email != string.Empty)
            {
                var existingUserName = await this.ExistingUserNameAsync(registrationInformation.UserName);
                var existingEmail = await this.ExistingEmailAsync(registrationInformation.Email);
                if (!existingUserName && !existingEmail && result.IsValid)
                {
                    await this.RegisterAsync(registrationInformation);
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> CheckUserExistsAsync(LoginDataModel credentials)
        {
            return await this.context.Login.AnyAsync(_ => _.UserName == this.encryptionService.Encryption(credentials.UserName) 
            && _.UserPassword == this.encryptionService.Encryption(credentials.UserPassword));
        }

        private async Task RegisterAsync(RegisterDataModel registrationInformation)
        {
            var loginInfo = new LoginDataModel() 
            { 
                UserName = this.encryptionService.Encryption(registrationInformation.UserName), 
                UserPassword = this.encryptionService.Encryption(registrationInformation.UserPassword) 
            };
            await this.context.Login.AddAsync(loginInfo);

            var registerInfo = new RegisterDataModel()
            {
                FirstName = registrationInformation.FirstName,
                LastName = registrationInformation.LastName,
                Email = registrationInformation.Email,
                PhoneNumber = registrationInformation.PhoneNumber,
                UserName = this.encryptionService.Encryption(registrationInformation.UserName),
                UserPassword = this.encryptionService.Encryption(registrationInformation.UserPassword)
            };
            await this.context.Register.AddAsync(registerInfo);
            await this.context.SaveChangesAsync();
        }

        private async Task<bool> ExistingUserNameAsync(string userName)
        {
            return  await this.context.Register.AnyAsync(x => x.UserName == userName);
        }

        private async Task<bool> ExistingEmailAsync(string email)
        {
            return await this.context.Register.AnyAsync(x => x.Email == email);
        }
    }
}
