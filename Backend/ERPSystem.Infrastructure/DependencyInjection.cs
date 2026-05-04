using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Infrastructure.Persistence;
using ERPsystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERPsystem.Infrastructure
{
    /// <summary>
    /// نقطة تسجيل خدمات طبقة البنية التحتية (Infrastructure).
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // تسجيل الـ DbContext مع SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // ربط الواجهة بالتنفيذ الفعلي (Scoped) ليتم حقنها في الـ Handlers
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            // تسجيل خدمة الملفات
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
