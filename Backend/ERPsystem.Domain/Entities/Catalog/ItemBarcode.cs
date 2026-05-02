using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Catalog;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// تعدد الباركودات للصنف (للحبة، الصندوق أو الكرتون).
    /// ميزة محورية جداً لنقاط البيع السريعة في قطاع التجزئة الحديث (سوبرماركت، صيدليات).
    /// </summary>
    public class ItemBarcode : TenantBaseEntity
    {
        /// <summary>معرف الصنف المربوط بهذا المقود (الباركود)</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف</summary>
        public virtual Item Item { get; set; } = null!;

        /// <summary>المقاس أو المتغير المربوط به إذا كان باركود لمقاس XL ولون مميز (لمحلات الملابس)</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>وحدة المتغير للصنف (مثال: تيشرت أحمر)</summary>
        public virtual ItemVariant? Variant { get; set; }

        /// <summary>شريط الرمز الكودي (الأرقام أو الحروف) للباركود</summary>
        public string Barcode { get; set; } = null!;
        
        /// <summary>بروتوكول الباركود الكوني المستخدم للرقم</summary>
        public BarcodeType BarcodeType { get; set; } = BarcodeType.EAN13;

        /// <summary>معرف واجهة الوحدة التي سيتم قراءتها بعد مسح هذا الباركود (هل باركود حبة أم باركود درزن)</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>الوحدة القياسية</summary>
        public virtual UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>عنصر الضرب للوحدة لتقليل مخزون الصنف الأساسي (مثلا: إذا قرأت باركود الكرتون فالقيمة 24 حبة)</summary>
        public decimal ConversionQty { get; set; } = 1;

        /// <summary>هل هذا الباركود هو الأساسي الافتراضي لتعريفه في طباعة الاستيكرات</summary>
        public bool IsPrimary { get; set; } = false;
    }
}
