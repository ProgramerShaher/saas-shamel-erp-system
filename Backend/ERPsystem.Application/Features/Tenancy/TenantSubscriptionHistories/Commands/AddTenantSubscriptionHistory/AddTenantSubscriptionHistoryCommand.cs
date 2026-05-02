using System;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Commands.AddTenantSubscriptionHistory
{
    /// <summary>
    /// أمر إضافة سجل جديد لتاريخ اشتراكات المنشأة.
    /// </summary>
    public class AddTenantSubscriptionHistoryCommand : IRequest<BaseResponse<Guid>>
    {
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }
        public SubscriptionChangeType ChangeType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? InvoiceRef { get; set; }
        public string? Notes { get; set; }
    }
}
