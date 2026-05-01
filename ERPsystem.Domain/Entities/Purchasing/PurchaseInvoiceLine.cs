using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// السطر التفصيلي للصنف داخل فاتورة المشتريات (يحدد التكلفة الدقيقة والضريبة).
    /// </summary>
    public class PurchaseInvoiceLine : TenantBaseEntity
    {
        /// <summary>الفاتورة الأساسية</summary>
        public Guid PurchaseInvoiceId { get; set; }
        
        /// <summary>فاتورة الشراء</summary>
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = null!;

        /// <summary>الصنف</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>المادة المشتراة</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>المستودع الذي تم توجيه البضاعة له مباشرة</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>المتغير في حالة الشراء للملابس مثلا</summary>
        public Guid? VariantId { get; set; }

        /// <summary>وحدة الشراء كالصندوق وكميته</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>وحدة المشتريات</summary>
        public virtual Catalog.UnitOfMeasure Unit { get; set; } = null!;

        /// <summary>كمية الشراء المعتمدة</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>سعر شراء الوحدة الواحدة</summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>مقدار الخصم على السطر الواحد</summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>مقدار الضريبة المضافة على السطر الواحد</summary>
        public decimal TaxAmount { get; set; }
        
        /// <summary>السعر الإجمالي الصافي الخاص بهذا السطر</summary>
        public decimal LineTotal => (Quantity * UnitPrice) - DiscountAmount + TaxAmount;

        /// <summary>رقم تشغيلة الدفعة إذا كانت تعمل كدواء</summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>تاريخ انتهاء الصلاحية للبضاعة المدخلة</summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
