using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Configuration;

namespace ERPsystem.Domain.Entities.Configuration
{
    /// <summary>
    /// سجل التدقيق ومراقبة المشرفين (System Audit Logs / Activity History).
    /// </summary>
    public class AuditLog : TenantBaseEntity
    {
        /// <summary>المستخدم الذي قام بالحدث</summary>
        public Guid UserId { get; set; }

        /// <summary>توقيت الحدث بالضبط</summary>
        public DateTime ActionAt { get; set; } = DateTime.UtcNow;

        /// <summary>طبيعة الحدث (نمذجة، قراءة، تعديل، إلغاء)</summary>
        public AuditAction Action { get; set; }

        /// <summary>اسم الكيان أو الوحدة المتأثرة (مثال: SalesInvoice, Item)</summary>
        public string EntityName { get; set; } = null!;
        
        /// <summary>معرف السجل المتأثر</summary>
        public Guid EntityId { get; set; }

        /// <summary>سجل التغييرات الكامل للقيم السابقة في هيئة جيسون لحفظ الحقوق</summary>
        public string? OldValuesJson { get; set; }
        
        /// <summary>القيم الجديدة بعد الحدث</summary>
        public string? NewValuesJson { get; set; }

        /// <summary>الآي بي والمكان الذي تم التعديل منه</summary>
        public string? IpAddress { get; set; }
    }
}
