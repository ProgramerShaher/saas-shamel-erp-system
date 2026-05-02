using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    /// <summary>
    /// معالج إنشاء باقة اشتراك جديدة. النقل بين الـ Command والـ Entity يتم يدوياً.
    /// </summary>
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateSubscriptionPlanCommandHandler> _logger;

        public CreateSubscriptionPlanCommandHandler(
            IApplicationDbContext context,
            ILogger<CreateSubscriptionPlanCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ إنشاء باقة اشتراك جديدة | الاسم: [{PlanName}]", request.PlanName);

            if (await _context.SubscriptionPlans.AnyAsync(p => p.PlanName.ToLower() == request.PlanName.ToLower(), cancellationToken))
            {
                _logger.LogWarning("⚠ فشل إنشاء الباقة — يوجد باقة مسبقة بنفس الاسم: [{PlanName}]", request.PlanName);
                return new BaseResponse<Guid>($"يوجد بالفعل باقة بهذا الاسم '{request.PlanName}'.");
            }

            var plan = new SubscriptionPlan
            {
                PlanName = request.PlanName,
                Description = request.Description,
                MonthlyPrice = request.MonthlyPrice,
                AnnualPrice = request.AnnualPrice,
                MaxBranches = request.MaxBranches,
                MaxPosTerminals = request.MaxPosTerminals,
                MaxUsers = request.MaxUsers,
                MaxItems = request.MaxItems,
                AllowedModulesJson = request.AllowedModulesJson,
                IsActive = request.IsActive
            };

            await _context.SubscriptionPlans.AddAsync(plan, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم إنشاء باقة الاشتراك بنجاح | المعرف: [{PlanId}]", plan.Id);

            return new BaseResponse<Guid>(plan.Id, "تم إنشاء الباقة بنجاح.");
        }
    }
}
