using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// قوائم التسعير المتخصصة لتمييز أطراف البيع والشراء المختلفة.
    /// يدعم قائمة أسعار الجملة، التجزئة، سعر كبار العملاء، وغيرها.
    /// </summary>
    public class PriceList : TenantBaseEntity
    {
        /// <summary>اسم قائمة التسعير التجاري (مثال: قايمة الجملة الذهبية)</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>عملة التسعير المعتمدة لهذه اللائحة</summary>
        public string CurrencyCode { get; set; } = "SAR";
        
        /// <summary>هل هي لائحة الأسعار الافتراضية للكاشير اليومي والزبون العام</summary>
        public bool IsDefault { get; set; } = false;
        
        /// <summary>حالة الموافقة وإعمال السياسة التسعيرية للائحة</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>جميع أسعار الأصناف التي تم التعديل عليها داخل هذه اللائحة</summary>
        public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();
    }
}
