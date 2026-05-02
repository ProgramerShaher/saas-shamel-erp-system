using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// السعر المحدد للصنف داخل لائحة الأسعار لكل وحدة.
    /// </summary>
    public class ItemPrice : TenantBaseEntity
    {
        /// <summary>الصنف المراد تغيير سعره بموجب اللائحة</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف المرتبط</summary>
        public virtual Item Item { get; set; } = null!;

        /// <summary>معرف اللائحة المستظل تحتها هذا السعر (سعر الجملة مثلا)</summary>
        public Guid PriceListId { get; set; }
        
        /// <summary>اللائحة الشجرية</summary>
        public virtual PriceList PriceList { get; set; } = null!;

        /// <summary>وحدة القياس التي خصص أُفرد لها السعر (سعر الكرتون مختلف عن سعر الحبة في نفس اللائحة)</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>وحدة المعايرة والتسعير</summary>
        public virtual UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>السعر النهائي المحسوم لهذا البند وتلك اللائحة</summary>
        public decimal Price { get; set; }
        
        /// <summary>أقل كمية مشترط أخذها ليتم تفعيل هذا التسعير على الفاتورة التلقائية (شرط سياسة بيع)</summary>
        public decimal? MinQty { get; set; }

        /// <summary>تاريخ سريان هذا التسعير أو بداية التخفيضات (العروض الموسمية)</summary>
        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
        
        /// <summary>تاريخ انتهاء عرض السعر أو اللائحة (وقت إقفال التخفيضات)</summary>
        public DateTime? ValidTo { get; set; }
    }
}
