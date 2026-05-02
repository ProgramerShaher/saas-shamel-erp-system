using ERPsystem.Domain.Entities.Tenancy;
using System;

namespace ERPsystem.Domain.Common
{
    /// <summary>
    /// الكيان الأساسي لجميع الجداول التي تنتمي لمنشأة معينة (Tenant-Scoped).
    /// يضمن عزل البيانات في بيئة Multi-Tenant بحيث لا تتداخل بيانات عميل مع عميل آخر.
    /// </summary>
    public abstract class TenantBaseEntity : BaseEntity
    {
        /// <summary>معرف المنشأة (المستأجر) الذي يملك هذا السجل</summary>
        public Guid TenantId { get; set; }
        
        /// <summary>الكيان الملاحي للمنشأة (التطرق لجدول المنشآت)</summary>
        public virtual Tenant Tenant { get; set; } = null!;
    }
}
