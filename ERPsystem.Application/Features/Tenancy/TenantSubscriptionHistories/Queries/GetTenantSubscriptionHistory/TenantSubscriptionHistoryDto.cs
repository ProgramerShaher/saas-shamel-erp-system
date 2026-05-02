using System;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory
{
    /// <summary>
    /// كائن نقل البيانات الخاص بتاريخ الاشتراكات.
    /// </summary>
    public class TenantSubscriptionHistoryDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }
        public SubscriptionChangeType ChangeType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? InvoiceRef { get; set; }
        public string? Notes { get; set; }
    }
}
