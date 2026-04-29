using System;
using ERPsystem.Domain.Entities;

namespace ERPsystem.Domain.Common
{
    /// <summary>
    /// الكيان الأساسي المخصص للمنشآت (Tenants). ترث منه جميع الكيانات التي تعود بياناتها لمنشأة معينة.
    /// يضمن العزل التام لبيانات العملاء في النظام السحابي متعدد المستأجرين (Multi-Tenant).
    /// </summary>
    public abstract class TenantBaseEntity : BaseEntity
    {
        /// <summary>المعرف الخاص بالمنشأة/المستأجر الذي يملك هذا السجل.</summary>
        public Guid TenantId { get; set; }

        /// <summary>المنشأة المالكة.</summary>
        public virtual Tenant Tenant { get; set; } = null!;
    }
}
