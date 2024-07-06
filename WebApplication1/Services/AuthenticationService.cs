namespace WebApplication1.Services
{
    using Microsoft.EntityFrameworkCore;

    using WebApplication1.Data;
    using WebApplication1.Models;
    using WebApplication1.Validators;

    public interface IAuthenticationService
    {
        Task<bool> CanLoginAsync(LoginModel credentials);

        Task<bool> HasRegisteredAsync(RegisterModel registrationInformation);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly PlannerContext context;
        private readonly IEncryptionService encryptionService;
        // Move this to app settings
        private readonly string Connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DailyPlannerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public AuthenticationService(IEncryptionService encryptionService, PlannerContext context)
        {
            this.encryptionService = encryptionService;
            this.context = context;
        }

        public async Task<bool> CanLoginAsync(LoginModel credentials)
        {
            LoginValidator validator = new LoginValidator();
            var result = validator.Validate(credentials);

            if (result.IsValid) {
                return await this.CheckUserExistsAsync(credentials);
            }

            return result.IsValid;
        }

        public async Task<bool> HasRegisteredAsync(RegisterModel registrationInformation)
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

        private async Task<bool> CheckUserExistsAsync(LoginModel credentials)
        {
            return await this.context.Login.AnyAsync(_ => _.UserName == this.encryptionService.Encryption(credentials.UserName) 
            && _.UserPassword == this.encryptionService.Encryption(credentials.UserPassword));
            //var sqlConnection = new SqlConnection(this.Connection);
            //var checkUser = new SqlCommand("SELECT * FROM UserTable WHERE ([UserName] = @userName) AND ([UserPassword] = @userPassword)", sqlConnection);
            //sqlConnection.Open();
            //checkUser.Parameters.AddWithValue("@userName", this.encryptionService.Encryption(credentials.UserName));
            //checkUser.Parameters.AddWithValue("@userPassword", this.encryptionService.Encryption(credentials.Password));
            //var reader = checkUser.ExecuteReader();
            //return reader.HasRows;
        }

        private async Task RegisterAsync(RegisterModel registrationInformation)
        {
            var loginInfo = new LoginModel() 
            { 
                UserName = this.encryptionService.Encryption(registrationInformation.UserName), 
                UserPassword = this.encryptionService.Encryption(registrationInformation.UserPassword) 
            };
            await this.context.Login.AddAsync(loginInfo);

            var registerInfo = new RegisterModel()
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
            //var sqlConnection = new SqlConnection(this.Connection);
            //var registerUser = new SqlCommand("INSERT INTO UserTable ([UserName], [UserPassword]) VALUES (@userName, @userPassword)", sqlConnection);
            //sqlConnection.Open();
            //registerUser.Parameters.AddWithValue("@userName", this.encryptionService.Encryption(registrationInformation.UserName));
            //registerUser.Parameters.AddWithValue("@userPassword", this.encryptionService.Encryption(registrationInformation.Password));
            //registerUser.ExecuteNonQuery();

            //var registerUserinfo = new SqlCommand("INSERT INTO UserInformation ([FirstName], [LastName], [PhoneNumber], [Email]) VALUES (@firstName, @lastName, @phoneNumber, @email)", sqlConnection);
            //registerUserinfo.Parameters.AddWithValue("@firstName", this.encryptionService.Encryption(registrationInformation.FirstName));
            //registerUserinfo.Parameters.AddWithValue("@lastName", this.encryptionService.Encryption(registrationInformation.LastName));
            //registerUserinfo.Parameters.AddWithValue("@phoneNumber", this.encryptionService.Encryption(registrationInformation.PhoneNumber));
            //registerUserinfo.Parameters.AddWithValue("@email", this.encryptionService.Encryption(registrationInformation.Email));
            //registerUserinfo.ExecuteNonQuery();
            //sqlConnection.Close();
        }

        private async Task<bool> ExistingUserNameAsync(string userName)
        {
            return  await this.context.Register.AnyAsync(x => x.UserName == userName);
            //var sqlConnection = new SqlConnection(this.Connection);
            //var checkUser = new SqlCommand("SELECT * FROM UserTable WHERE ([UserName] = @userName)", sqlConnection);
            //sqlConnection.Open();
            //checkUser.Parameters.AddWithValue("@userName", this.encryptionService.Encryption(userName));
            //var reader = checkUser.ExecuteReader();
            //return reader.HasRows;
        }

        private async Task<bool> ExistingEmailAsync(string email)
        {
            return await this.context.Register.AnyAsync(x => x.Email == email);
            //var sqlConnection = new SqlConnection(this.Connection);
            //var checkEmail = new SqlCommand("SELECT * FROM UserInformation WHERE ([Email] = @email)", sqlConnection);
            //sqlConnection.Open();
            //checkEmail.Parameters.AddWithValue("@email", this.encryptionService.Encryption(email));
            //var reader = checkEmail.ExecuteReader();
            //return reader.HasRows;
        }
    }
}
