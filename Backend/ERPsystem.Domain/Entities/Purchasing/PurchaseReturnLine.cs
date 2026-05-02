using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// السطر التفصيلي داخل مرتجع المشتريات.
    /// </summary>
    public class PurchaseReturnLine : TenantBaseEntity
    {
        /// <summary>المرتجع الأب</summary>
        public Guid PurchaseReturnId { get; set; }
        
        /// <summary>مرتجع المشتريات المرتبط</summary>
        public virtual PurchaseReturn PurchaseReturn { get; set; } = null!;

        /// <summary>السطر الأساسي في الفاتورة التي ارجعنا منه للتتبع والمطابقة</summary>
        public Guid PurchaseInvoiceLineId { get; set; }
        
        /// <summary>الصنف</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>السلعة المعادة</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>الكمية المسترجعة</summary>
        public decimal ReturnedQty { get; set; }
        
        /// <summary>سعر الاسترداد المتفق عليه (غالبا سعر الشراء القديم)</summary>
        public decimal UnitRefundPrice { get; set; }
        
        /// <summary>تراجع الضريبة المتعلقة بالكمية المستردة</summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>الإجمالي الجزئي للسطر</summary>
        public decimal LineTotal => (ReturnedQty * UnitRefundPrice) + TaxAmount;

        /// <summary>سبب الإرجاع</summary>
        public string? ReturnReason { get; set; }
    }
}
