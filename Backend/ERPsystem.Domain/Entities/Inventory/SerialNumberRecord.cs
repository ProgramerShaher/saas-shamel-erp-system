using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Inventory;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// أرقام التسلسل الثابتة للأجهزة (Serial Numbers).
    /// تستخدم لربط الضمان ومراقبة كل حبة جهاز بذاتها (لتجار الإلكترونيات والسيارات والجوالات).
    /// </summary>
    public class SerialNumberRecord : TenantBaseEntity
    {
        /// <summary>رقم هوية المنتج</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>موقع المستودع المتواجد به القطعة</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>الرمز الشريطي للرقم التسلسلي المسجل بالجهاز نفسه (رقم الهاتف، الماك ادرس)</summary>
        public string SerialNumber { get; set; } = null!;
        
        /// <summary>موقف السيريال: هل موجود، أم مباع، أم مرتجع؟</summary>
        public SerialNumberStatus Status { get; set; } = SerialNumberStatus.InStock;

        /// <summary>سطر فاتورة الشراء (لنعرف متى وممن تم شراء القطعة بالظبط)</summary>
        public Guid? PurchaseInvoiceLineId { get; set; }
        
        /// <summary>سطر فاتورة البيع (لنعرف متى وبكم مباعة هذه القطعة المحددة)</summary>
        public Guid? SaleInvoiceLineId { get; set; }

        /// <summary>تاريخ خروج القطعة من الضمان ليتم التحقق منه تلقائيا</summary>
        public DateTime? WarrantyExpiryDate { get; set; }
        
        /// <summary>ملاحظات العيوب إن كان به ديفو أو مرتجع للصيانة</summary>
        public string? Notes { get; set; }
    }
}
