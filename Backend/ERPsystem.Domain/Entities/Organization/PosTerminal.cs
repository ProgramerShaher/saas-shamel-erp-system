using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// نقطة البيع (الصندوق أو جهاز الكاشير الملموس) داخل الفرع.
    /// </summary>
    public class PosTerminal : TenantBaseEntity
    {
        /// <summary>معرف الفرع المعرّف فيه هذا الجهاز</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع المحتضن لجهاز الكاشير</summary>
        public virtual Branch Branch { get; set; } = null!;

        /// <summary>الرقم التعريفي للجهاز (مثال: POS-01)</summary>
        public string TerminalCode { get; set; } = null!;
        
        /// <summary>اسم الكاشير كمنطقة عمل (كاشير العطورات، كاشير الإكسسوارات)</summary>
        public string Name { get; set; } = null!;

        /// <summary>حالة العمل للجهاز</summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>عنوان الشبكة المحلية للتواصل مع الجهاز إن طلب</summary>
        public string? IpAddress { get; set; }
        
        /// <summary>العنوان الآلي للمطابقة الأمنية (MAC Address)</summary>
        public string? MacAddress { get; set; }
        
        /// <summary>اسم الطابعة الحرارية المتصلة بالجهاز للإرسال المباشر للرسيد</summary>
        public string? PrinterName { get; set; }

        /// <summary>المستودع الافتراضي لسحب كمية المواد المباعة عبر الباركود مباشرة وتقليل الرصيد</summary>
        public Guid DefaultWarehouseId { get; set; }
        
        /// <summary>المستودع الافتراضي المرتبط بخصم المبيعات</summary>
        public virtual Warehouse DefaultWarehouse { get; set; } = null!;
    }
}
