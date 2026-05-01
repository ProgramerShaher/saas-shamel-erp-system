using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// العلامات (الماركات) التجارية للأصناف.
    /// مهمة لعمليات البحث وفلترة المبيعات وطلبيات الموردين.
    /// </summary>
    public class Brand : TenantBaseEntity
    {
        /// <summary>اسم الماركة (مثال: سامسونج، كوكاكولا)</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>الصناعة أو بلد المنشأ الخاص بالماركة</summary>
        public string? Country { get; set; }
        
        /// <summary>رابط لشعار العلامة التجارية لعرضه في نقاط البيع والتطبيق</summary>
        public string? LogoUrl { get; set; }
        
        /// <summary>حالة التعامل مع هذه الماركة</summary>
        public bool IsActive { get; set; } = true;
    }
}
