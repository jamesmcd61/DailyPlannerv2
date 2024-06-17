namespace DailyPlanner.Services
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddPlannerServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IPlannerService, PlannerService>();

            return services;
        }
    }
}
