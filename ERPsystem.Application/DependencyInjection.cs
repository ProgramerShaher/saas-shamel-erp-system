using System.Reflection;
using AutoMapper;
using ERPsystem.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ERPsystem.Application
{
    /// <summary>
    /// نقطة تسجيل جميع خدمات طبقة الـ Application في حاوية الـ Dependency Injection.
    /// يتم استدعاؤها مرة واحدة من طبقة الـ API في Startup/Program.cs.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// تسجيل MediatR، AutoMapper، FluentValidation، والـ Pipeline Behaviors.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // تسجيل MediatR وجميع الـ Handlers والـ Commands تلقائياً
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // تسجيل AutoMapper — يكتشف جميع Profiles الموزعة داخل Features تلقائياً
            // يُطبق قاعدة أولاً من الدستور: كل Feature تملك Profile مستقل خاص بها
            services.AddAutoMapper(assembly);

            // تسجيل جميع الـ Validators تلقائياً من طبقة الـ Application
            services.AddValidatorsFromAssembly(assembly);

            // تسجيل الـ Pipeline Behaviors بالترتيب الصحيح كما في الدستور:
            // Logging → Validation → (Handler)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
