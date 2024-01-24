using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Common.Confifurations;
using Sprout.Exam.Common.Mappers;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.DataAccess.Repositories;
using Sprout.Exam.WebApp.Data;

namespace Sprout.Exam.WebApp.DI
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Dependency Injection
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IIncomeService, IncomeService>();

            // AutoMapper
            var mapper = AutoMapperConfig.Initialize();
            services.AddSingleton(mapper);
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<SproutDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void RegisterSalarySettings(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<SalarySettings>(Configuration.GetSection("SalarySettings"));
        }
    }
}
