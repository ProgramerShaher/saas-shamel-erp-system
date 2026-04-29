using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل الصنف أو المنتج المعروض للبيع أو الشراء.
    /// </summary>
    public class Product : TenantBaseEntity
    {
        /// <summary>اسم المنتج</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>رمز الباركود الخاص بالمنتج (يستخدم في نقطة البيع)</summary>
        public string? Barcode { get; set; }
        
        /// <summary>وصف إضافي للمنتج</summary>
        public string? Description { get; set; }
        
        /// <summary>معرف التصنيف الذي ينتمي إليه المنتج</summary>
        public Guid CategoryId { get; set; }
        
        /// <summary>معرف وحدة قياس المنتج</summary>
        public Guid UnitId { get; set; }
        
        /// <summary>متوسط سعر الشراء والتكلفة</summary>
        public decimal PurchasePrice { get; set; }
        
        /// <summary>سعر البيع الافتراضي للعميل</summary>
        public decimal SalePrice { get; set; }
        
        /// <summary>الرصيد الحالي المتوفر من هذا المنتج في المخزن</summary>
        public decimal CurrentStock { get; set; }
        
        /// <summary>حد إعادة الطلب، عند وصول الكمية لهذا الحد يتم التنبيه</summary>
        public decimal MinStockLevel { get; set; }
        
        /// <summary>حالة المنتج، إذا كان معروضاً للبيع أم موقوفاً</summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>رابط صورة المنتج إن وجد</summary>
        public string? ImageUrl { get; set; }

        // Navigation
        public virtual Category Category { get; set; } = null!;
        public virtual Unit Unit { get; set; } = null!;
        public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
        public virtual ICollection<SaleInvoiceItem> SaleInvoiceItems { get; set; } = new List<SaleInvoiceItem>();
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
