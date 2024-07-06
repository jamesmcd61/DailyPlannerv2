namespace WebApplication1
{
    using WebApplication1.Data;
    using WebApplication1.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var dbConnection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionSql").Value;
            services.AddDbContext<PlannerContext>(_ => _.UseSqlServer(dbConnection));
            services.AddPlannerServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowSpecificOrigin");
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpMethodOverride();
            app.UseAuthorization();
            app.UseRouting();
        }
    }
}
