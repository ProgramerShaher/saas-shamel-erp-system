using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Commands.AddTenantSubscriptionHistory
{
    /// <summary>
    /// معالج إضافة سجل تاريخي إضافي.
    /// </summary>
    public class AddTenantSubscriptionHistoryCommandHandler : IRequestHandler<AddTenantSubscriptionHistoryCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AddTenantSubscriptionHistoryCommandHandler> _logger;

        public AddTenantSubscriptionHistoryCommandHandler(
            IApplicationDbContext context,
            ILogger<AddTenantSubscriptionHistoryCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(AddTenantSubscriptionHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ تسجيل تاريخ اشتراك للمنشأة [{TenantId}] للباقة [{PlanId}]", request.TenantId, request.PlanId);

            var historyInfo = new TenantSubscriptionHistory
            {
                TenantId = request.TenantId,
                PlanId = request.PlanId,
                ChangeType = request.ChangeType,
                EffectiveDate = request.EffectiveDate,
                InvoiceRef = request.InvoiceRef,
                Notes = request.Notes
            };

            await _context.TenantSubscriptionHistories.AddAsync(historyInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم تسجيل حركة الاشتراك بنجاح | المعرف: [{HistoryId}]", historyInfo.Id);
            return new BaseResponse<Guid>(historyInfo.Id, "تم تسجيل تاريخ الاشتراك بنجاح.");
        }
    }
}
