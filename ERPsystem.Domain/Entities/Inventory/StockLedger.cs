using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Inventory;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// دفتر أستاذ المخزون (Stock Ledger / History).
    /// الجوهر الأساسي لسلسلة الإمداد: سجل غير قابل للمسح أو التغيير يحفظ كل حركة حدثت للمخزون (دخول/خروج).
    /// أي كمية متوفرة اليوم في البرنامج هي حاصل جمع جميع الحركات في هذا الدفتر.
    /// </summary>
    public class StockLedger : TenantBaseEntity
    {
        /// <summary>المنتج الذي تم تحريكه</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف الأم المعني بالتحريك</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>الخيار/المقاس المحدد المحرك (إن وجد)</summary>
        public Guid? VariantId { get; set; }
        
        /// <summary>الصنف المتغير إن وجد</summary>
        public virtual Catalog.ItemVariant? Variant { get; set; }

        /// <summary>المستودع الذي تم صرف أو إضافة الصنف إليه</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع الفعلي الملموس</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>دلالية الحركة التوجيهية (شراء وارد، بيع صادر، تحويل، إتلاف)</summary>
        public StockMovementType MovementType { get; set; }

        /// <summary>الجهة ومصدر الأمر المترجم (PurchaseInvoice / SalesInvoice / StockTransfer)</summary>
        public string SourceDocumentType { get; set; } = null!;
        
        /// <summary>معرف المستند (توجيهك لفاتورة البيع ذاتها أو فاتورة الشراء)</summary>
        public Guid SourceDocumentId { get; set; }
        
        /// <summary>السطر (Item Line) داخل المستند المولد لهذه الحركة</summary>
        public Guid? SourceLineId { get; set; }

        /// <summary>حجم التغيير بالكمية الموجبة (الدخول والإيرادات) والسالبة (للصرف والمبيعات نقصاً)</summary>
        public decimal Quantity { get; set; }

        /// <summary>الوحدة التي تمت الحركة بها لتحويل القيمة لاحقاً</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>وحدة المعايرة الفعالة للحركة المرفوعة</summary>
        public virtual Catalog.UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>تكلفة الوحدة الواحدة من الصنف وقت وقوع هذه الحركة لتقييم الميزانية بشكل دقيق</summary>
        public decimal UnitCost { get; set; }
        
        /// <summary>التكلفة الإجمالية للحركة المدخلة/المنصرفة</summary>
        public decimal TotalCost { get; set; }

        // Feature Flag Fields (Batch, Serial)
        /// <summary>رقم تشغيلة/باتش الدفعة للحركة (لتطبيقات الصيدليات)</summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>تاريخ الانتهاء الموازي للكمية المدخلة أو المنصرفة</summary>
        public DateTime? ExpiryDate { get; set; }
        
        /// <summary>الرقم التسلسلي للجهاز أو القطعة (خاصة بالإلكترونيات)</summary>
        public string? SerialNumber { get; set; }

        /// <summary>تاريخ توقيت الحركة بالتاريخ التفصيلي</summary>
        public DateTime MovedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>المستخدم أو أمين المستودع المعتمد والمنفذ للصرف/الإدخال</summary>
        public Guid MovedByUserId { get; set; }

        /// <summary>القيد اليومي المالي الذي يعكس حركة الدفتر هذه للربط المحاسبي التام</summary>
        public Guid? JournalEntryId { get; set; }
    }
}
