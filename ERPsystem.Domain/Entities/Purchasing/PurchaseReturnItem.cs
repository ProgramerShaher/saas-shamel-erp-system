using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل الأصناف المرجعة للمورد.
    /// </summary>
    public class PurchaseReturnItem : TenantBaseEntity
    {
        public Guid PurchaseReturnId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }

        // Navigation
        public virtual PurchaseReturn PurchaseReturn { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
