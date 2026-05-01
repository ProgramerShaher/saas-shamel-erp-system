using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Purchasing;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// أمر الشراء (Purchase Order) — طلب مبدئي من المورد لتوفير البضاعة وتحديد أسعارها (ليس له تأثير محاسبي فوري).
    /// </summary>
    public class PurchaseOrder : TenantBaseEntity
    {
        /// <summary>رقم التعميد أو أمر الشراء</summary>
        public string OrderNumber { get; set; } = null!;
        
        /// <summary>المورد الموجه إليه الطلب</summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>المورد</summary>
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>الفرع والطالب</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع المستفيد</summary>
        public virtual Organization.Branch Branch { get; set; } = null!;

        /// <summary>حالة الطلب (مسودة، مرسل، أو مستلم)</summary>
        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;

        /// <summary>تاريخ أمر الشراء</summary>
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>التاريخ المتوقع للاستلام</summary>
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>إجمالي مبلغ التعميد المرجو</summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>ملاحظات أمر الشراء أو شروط التعميد</summary>
        public string? Notes { get; set; }

        /// <summary>الأسطر المطلوبة في الطلبية</summary>
        public virtual ICollection<PurchaseOrderLine> Lines { get; set; } = new List<PurchaseOrderLine>();
    }
}
