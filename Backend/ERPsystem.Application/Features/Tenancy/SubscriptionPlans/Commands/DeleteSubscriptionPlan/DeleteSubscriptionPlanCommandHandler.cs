using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.DeleteSubscriptionPlan
{
    /// <summary>
    /// معالج أمر حذف الباقة.
    /// </summary>
    public class DeleteSubscriptionPlanCommandHandler : IRequestHandler<DeleteSubscriptionPlanCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DeleteSubscriptionPlanCommandHandler> _logger;

        public DeleteSubscriptionPlanCommandHandler(
            IApplicationDbContext context,
            ILogger<DeleteSubscriptionPlanCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(DeleteSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ حذف الباقة | المعرف: [{PlanId}]", request.Id);

            var plan = await _context.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (plan == null)
            {
                _logger.LogWarning("⚠ فشل الحذف — الباقة غير موجودة | المعرف: [{PlanId}]", request.Id);
                return new BaseResponse<Guid>("باقة الاشتراك غير موجودة.");
            }

            // يتم حذف الكيان (وإن كان هناك Soft Delete مهيأ برمجياً فسيتم تطبيقه أوتوماتيكياً).
            _context.SubscriptionPlans.Remove(plan);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم حذف الباقة بنجاح | المعرف: [{PlanId}]", plan.Id);
            return new BaseResponse<Guid>(plan.Id, "تم حذف الباقة بنجاح.");
        }
    }
}
