using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// السطر أو العينة المرتجعة الفردية من المبيعات.
    /// </summary>
    public class SalesReturnLine : TenantBaseEntity
    {
        /// <summary>وثيقة الاسترجاع الأم</summary>
        public Guid SalesReturnId { get; set; }
        
        /// <summary>وثيقة الاسترجاع</summary>
        public virtual SalesReturn SalesReturn { get; set; } = null!;

        /// <summary>سطر الفاتورة القديم ليطابق مع الكمية المباعة</summary>
        public Guid SalesInvoiceLineId { get; set; }
        
        /// <summary>المنتج المراد رده</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>المنتج</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>الكمية المسترجعة الصافية</summary>
        public decimal ReturnedQty { get; set; }
        
        /// <summary>السعر المعادل للإرجاع والذي اشتراه به العميل سابقا بالظبط</summary>
        public decimal UnitRefundPrice { get; set; }
        
        /// <summary>الضريبة المعكوسة للسطر</summary>
        public decimal TaxAmount { get; set; }

        /// <summary>سبب الإعادة (مقاس غير مناسب، منتهي الصلاحية، الخ)</summary>
        public string? ReturnReason { get; set; }
    }
}
