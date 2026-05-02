using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Catalog;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// الصنف/المنتج/السلعة — الكيان الجوهري لأي نظام مبيعات ومخازن.
    /// جميع فواتير الشراء والبيع وعمليات الجرد ترتبط بهذا الحجر الأساس.
    /// مفاتيح המميزات (Feature Flags) تغير من سلوك هذا الصنف.
    /// </summary>
    public class Item : TenantBaseEntity
    {
        /// <summary>مجموعة/فصيلة الصنف (ألبان، أدوات كهربائية)</summary>
        public Guid CategoryId { get; set; }
        
        /// <summary>التصنيف</summary>
        public virtual Category Category { get; set; } = null!;

        /// <summary>معرف الماركة التجارية المصنعة (اختياري)</summary>
        public Guid? BrandId { get; set; }
        
        /// <summary>الماركة التجارية</summary>
        public virtual Brand? Brand { get; set; }

        /// <summary>رقم/كود الصنف الداخلي للمنظمة ولا يتكرر</summary>
        public string Code { get; set; } = null!;
        
        /// <summary>اسم الصنف/المنتج الدقيق للطباعة والفواتير</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>الاسم العربي (في حال كانت الأسماء مزدوجة اللغات)</summary>
        public string? NameAr { get; set; }
        
        /// <summary>الوصف التعريفي التفصيلي ليظهر بمواقع التجارة الإلكترونية</summary>
        public string? Description { get; set; }

        /// <summary>نوع وتركيبة الصنف (بضائع مخزنية، خدمات، مصنعات)</summary>
        public ItemType ItemType { get; set; } = ItemType.Standard;

        /// <summary>الوحدة الأساسية الصغرى للصنف التي يقاس بها المخزون (حبّة غالباً)</summary>
        public Guid BaseUnitId { get; set; }
        
        /// <summary>الوحدة الأساسية</summary>
        public virtual UnitOfMeasure BaseUnit { get; set; } = null!;

        /// <summary>متوسط سعر التكلفة الابتدائي الموروث (يحسب أوتماتيكياً لاحقاً بناءً على طريقة التكلفة كالمتوسط المرجح)</summary>
        public decimal CostPrice { get; set; }
        
        /// <summary>سعر البيع الافتراضي لوحدة الصنف الأساسية للعميل النهائي قطاعي</summary>
        public decimal SalePrice { get; set; }

        /// <summary>الفئة الضريبية المطبقة على الصنف (15%، صفري)</summary>
        public Guid? TaxGroupId { get; set; }
        
        /// <summary>الضريبة</summary>
        public virtual TaxGroup? TaxGroup { get; set; }

        // Feature-Flag-Driven Fields
        /// <summary>هل هذا المنتج كالأدوية له تاريخ صلاحية يجب أن يُدخل إلزاميا؟</summary>
        public bool HasExpiryDate { get; set; } = false;

        /// <summary>هل هذا الصنف من الأدوية أو المنظفات وله رقم تشغيلة (باتش) لارجاع الدفعات الفاسدة؟</summary>
        public bool TrackBatchNumbers { get; set; } = false;

        /// <summary>هل هذا الصنف إلكترونيات يجب مسح الباركود التسلسلي (السيريال) الخاص بكل حبة منه للمخزن والضمان؟</summary>
        public bool TrackSerialNumbers { get; set; } = false;

        /// <summary>هل هذا الصنف نوع ملابس يحتوي مقاسات أو ألوان تضرب كأسماء فرعية؟</summary>
        public bool HasVariants { get; set; } = false;

        /// <summary>الحد الأدنى لطلب الموردين للتحذير من نفاذ المخزون</summary>
        public decimal MinStockLevel { get; set; } = 0;
        
        /// <summary>الحد الأقصى للتخزين لتفادي تكدس المستودع</summary>
        public decimal MaxStockLevel { get; set; } = 0;
        
        /// <summary>نقطة الطلب الآلي لإعادة تعميد المشتريات التلقائي</summary>
        public decimal ReorderPoint { get; set; } = 0;

        /// <summary>مسار صورة المنتج</summary>
        public string? ImageUrl { get; set; }
        
        /// <summary>حالة إمكانية تداول هذا الصنف وشراؤه وبيعه</summary>
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        /// <summary>جميع باركودات الصنف سواء للحبة أو الباكت</summary>
        public virtual ICollection<ItemBarcode> Barcodes { get; set; } = new List<ItemBarcode>();
        
        /// <summary>قائمة بعوامل تحويل الوحدات للصنف</summary>
        public virtual ICollection<ItemUnit> Units { get; set; } = new List<ItemUnit>();
        
        /// <summary>المقاسات والألوان المنبثقة من هذا الصنف الأساسي إن وجدت</summary>
        public virtual ICollection<ItemVariant> Variants { get; set; } = new List<ItemVariant>();
        
        /// <summary>أسعار الصنف المبنية على قوائم التسعير المختلفة</summary>
        public virtual ICollection<ItemPrice> Prices { get; set; } = new List<ItemPrice>();
    }
}
