using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// بيانات دفعات ولوطات الإنتاج للأصناف (Batch / Lots).
    /// ميزة حصرية تتبع الصيدليات والمواد الغذائية السريعة التلف لإخراج القديم أولاً (FIFO/FEFO).
    /// </summary>
    public class BatchLot : TenantBaseEntity
    {
        /// <summary>معرف الصنف الخاضع للمراقبة بتواريخ الصلاحية</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف الأم</summary>
        public virtual Catalog.Item Item { get; set; } = null!;

        /// <summary>المستودع الذي يحفظ هذه الدفعة</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>التسلسل الرقمي للتشغيلة واللوط والمورد</summary>
        public string BatchNumber { get; set; } = null!;
        
        /// <summary>تاريخ انتهاء وفساد هذه الدفعة بالتحديد (Expiry Date)</summary>
        public DateTime ExpiryDate { get; set; }
        
        /// <summary>تاريخ صنع وتوليد هذه الدفعة</summary>
        public DateTime? ManufactureDate { get; set; }

        /// <summary>كمية الحبات التي تم إدخالها عند الشراء لأول مرة لهذه الدفعة</summary>
        public decimal QuantityReceived { get; set; }
        
        /// <summary>كمية الحبات المتبقية والصالحة حتى اليوم (تنقص مع كل عملية بيع)</summary>
        public decimal QuantityRemaining { get; set; }
        
        /// <summary>تكلفة الوحدة للكمية المدخلة في الدفعة لتقسيم الجرد عند التلف</summary>
        public decimal CostPerUnit { get; set; }

        /// <summary>معرف فاتورة شراء الدفعة لمعرفة المصدر وقت الاحتياج للمورد</summary>
        public Guid PurchaseInvoiceId { get; set; }
    }
}
