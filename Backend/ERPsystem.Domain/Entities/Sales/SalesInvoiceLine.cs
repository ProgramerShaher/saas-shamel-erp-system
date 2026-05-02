using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// السطر المباع للعميل داخل الفاتورة.
    /// </summary>
    public class SalesInvoiceLine : TenantBaseEntity
    {
        /// <summary>الفاتورة التابع لها</summary>
        public Guid SalesInvoiceId { get; set; }
        
        /// <summary>الفاتورة الأم</summary>
        public virtual SalesInvoice SalesInvoice { get; set; } = null!;

        /// <summary>المنتج</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>المنتج المباع</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>المتغير والمقاس المخصص</summary>
        public Guid? VariantId { get; set; }

        /// <summary>وحدة المعايرة كالحبة أو الكرتون</summary>
        public Guid UnitId { get; set; }

        /// <summary>الكمية المباعة</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>سعر البيع المطبق</summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>التخفيض المندرج في السطر</summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>ضريبة السطر</summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>إجمالي صافي الربح من السطر</summary>
        public decimal LineTotal => (Quantity * UnitPrice) - DiscountAmount + TaxAmount;

        /// <summary>رقم التشغيلة للأدوية لتتبع خروجها</summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>الرقم التسلسلي للجهاز لضمان البيع</summary>
        public string? SerialNumber { get; set; }
    }
}
