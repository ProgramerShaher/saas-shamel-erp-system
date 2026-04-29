using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مستند الجرد وتسوية المخزون عند وجود عجز أو زيادة في المستودع.
    /// </summary>
    public class InventoryAdjustment : TenantBaseEntity
    {
        /// <summary>الرقم التسلسلي لمستند التسوية</summary>
        public string AdjustmentNumber { get; set; } = string.Empty;
        
        /// <summary>تاريخ الجرد والتسوية</summary>
        public DateTime AdjustmentDate { get; set; }
        
        /// <summary>ملاحظات إضافية حول التسوية</summary>
        public string? Notes { get; set; }
        
        /// <summary>حالة المستند، مسودة أم تم الاعتماد</summary>
        public string Status { get; set; } = "Draft";

        // Navigation
        public virtual ICollection<InventoryAdjustmentItem> Items { get; set; } = new List<InventoryAdjustmentItem>();
    }
}
