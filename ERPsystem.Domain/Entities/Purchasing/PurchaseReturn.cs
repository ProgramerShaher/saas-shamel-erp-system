using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مستند إرجاع مشتريات للمورد لرد البضاعة المعيبة مثلاً.
    /// </summary>
    public class PurchaseReturn : TenantBaseEntity
    {
        /// <summary>رقم المرتجع التسلسلي</summary>
        public string ReturnNumber { get; set; } = string.Empty;
        
        /// <summary>معرف الفاتورة الأصلية للمشتريات</summary>
        public Guid PurchaseInvoiceId { get; set; }
        
        /// <summary>تاريخ الإرجاع للمورد</summary>
        public DateTime ReturnDate { get; set; }
        
        /// <summary>المبلغ الإجمالي المسترد من المورد أو المخصوم من الرصيد</summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>سبب الإرجاع للمورد</summary>
        public string Reason { get; set; } = string.Empty;

        // Navigation
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = null!;
        public virtual ICollection<PurchaseReturnItem> Items { get; set; } = new List<PurchaseReturnItem>();
    }
}
