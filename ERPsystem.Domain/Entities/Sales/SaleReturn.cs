using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مستند إرجاع مبيعات (مرتجع).
    /// </summary>
    public class SaleReturn : TenantBaseEntity
    {
        /// <summary>رقم مستند المرتجع</summary>
        public string ReturnNumber { get; set; } = string.Empty;
        
        /// <summary>معرف الفاتورة الأصلية التي تم الإرجاع بناءً عليها</summary>
        public Guid SaleInvoiceId { get; set; }
        
        /// <summary>تاريخ الإرجاع</summary>
        public DateTime ReturnDate { get; set; }
        
        /// <summary>القيمة المالية الإجمالية للبضاعة المسترجعة</summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>سبب الإرجاع</summary>
        public string Reason { get; set; } = string.Empty;
        
        /// <summary>حالة الإرجاع (موافق، قيد الانتظار)</summary>
        public string Status { get; set; } = "Approved";

        // Navigation
        public virtual SaleInvoice SaleInvoice { get; set; } = null!;
        public virtual ICollection<SaleReturnItem> Items { get; set; } = new List<SaleReturnItem>();
    }
}
