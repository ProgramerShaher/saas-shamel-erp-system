using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Domain.Entities.Tenancy
{
    /// <summary>
    /// سجل تاريخي لجميع حركات تغيير الاشتراك للمنشأة.
    /// </summary>
    public class TenantSubscriptionHistory : BaseEntity
    {
        /// <summary>معرف المنشأة</summary>
        public Guid TenantId { get; set; }
        
        /// <summary>المنشأة</summary>
        public virtual Tenant Tenant { get; set; } = null!;

        /// <summary>معرف الباقة التي تم التحويل إليها</summary>
        public Guid PlanId { get; set; }
        
        /// <summary>الباقة المرتبطة المستهدفة</summary>
        public virtual SubscriptionPlan Plan { get; set; } = null!;

        /// <summary>طبيعة التغيير (تجديد، ترقية، إيقاف مؤقت)</summary>
        public SubscriptionChangeType ChangeType { get; set; }
        
        /// <summary>تاريخ بداية تفعيل التغيير</summary>
        public DateTime EffectiveDate { get; set; }
        
        /// <summary>رقم مرجعي للمدفوعات عبر طرف ثالث (مثل Stripe / PayTabs)</summary>
        public string? InvoiceRef { get; set; }
        
        /// <summary>ملاحظات الدعم الفني عن التجديد</summary>
        public string? Notes { get; set; }
    }
}
