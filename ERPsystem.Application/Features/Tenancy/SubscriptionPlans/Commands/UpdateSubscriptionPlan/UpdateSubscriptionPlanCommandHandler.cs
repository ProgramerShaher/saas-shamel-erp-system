using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.UpdateSubscriptionPlan
{
    /// <summary>
    /// معالج أمر التعديل على باقة الاشتراك.
    /// </summary>
    public class UpdateSubscriptionPlanCommandHandler : IRequestHandler<UpdateSubscriptionPlanCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateSubscriptionPlanCommandHandler> _logger;

        public UpdateSubscriptionPlanCommandHandler(
            IApplicationDbContext context,
            ILogger<UpdateSubscriptionPlanCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(UpdateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ تعديل باقة الاشتراك | المعرف: [{PlanId}]", request.Id);

            var plan = await _context.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("⚠ فشل التعديل — الباقة غير موجودة | المعرف: [{PlanId}]", request.Id);
                return new BaseResponse<Guid>("باقة الاشتراك غير موجودة.");
            }

            // يتم التعديل يدوياً حسب القواعد
            plan.PlanName = request.PlanName;
            plan.Description = request.Description;
            plan.MonthlyPrice = request.MonthlyPrice;
            plan.AnnualPrice = request.AnnualPrice;
            plan.MaxBranches = request.MaxBranches;
            plan.MaxPosTerminals = request.MaxPosTerminals;
            plan.MaxUsers = request.MaxUsers;
            plan.MaxItems = request.MaxItems;
            plan.AllowedModulesJson = request.AllowedModulesJson;
            plan.IsActive = request.IsActive;

            _context.SubscriptionPlans.Update(plan);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم تعديل الباقة بنجاح | المعرف: [{PlanId}]", plan.Id);

            return new BaseResponse<Guid>(plan.Id, "تم تعديل الباقة بنجاح.");
        }
    }
}
