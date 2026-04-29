using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// لتتبع الحركات المخزنية الواردة والصادرة وتسويات الجرد.
    /// </summary>
    public class StockMovement : TenantBaseEntity
    {
        /// <summary>معرف المنتج الخاص بالحركة</summary>
        public Guid ProductId { get; set; }
        
        /// <summary>كمية التحرك</summary>
        public decimal Quantity { get; set; }
        
        /// <summary>نوع الحركة (IN وارد / OUT صادر / ADJUST تسوية)</summary>
        public string MovementType { get; set; } = string.Empty;
        
        /// <summary>سبب الحركة كـ عمليات بيع أو شراء أو إرجاع أو تسوية</summary>
        public string Reason { get; set; } = string.Empty;
        
        /// <summary>معرف السجل المرجعي كالـ (فاتورة مبيعات، تسوية، إلخ)</summary>
        public Guid? ReferenceId { get; set; }
        
        /// <summary>نوع السجل المرجعي لتسهيل البحث</summary>
        public string? ReferenceType { get; set; }
        
        /// <summary>المخزون المتوفر قبل تنفيذ الحركة</summary>
        public decimal StockBefore { get; set; }
        
        /// <summary>المخزون المتبقي بعد تنفيذ الحركة</summary>
        public decimal StockAfter { get; set; }
        
        /// <summary>تاريخ ووقت حدوث الحركة</summary>
        public DateTime MovementDate { get; set; }

        // Navigation
        public virtual Product Product { get; set; } = null!;
    }
}
