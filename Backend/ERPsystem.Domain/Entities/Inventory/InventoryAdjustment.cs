using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Inventory;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// تسوية المخزون وإدارة الجرد (Inventory Adjustment / Stock Take).
    /// لمعالجة الهدر، المنتجات التالفة، وإدخال الأرصدة الافتتاحية وجرد نهاية العام.
    /// </summary>
    public class InventoryAdjustment : TenantBaseEntity
    {
        /// <summary>المستودع الذي تجري عليه عملية التسوية والجرد</summary>
        public Guid WarehouseId { get; set; }
        
        /// <summary>المستودع الجاري جرده</summary>
        public virtual Organization.Warehouse Warehouse { get; set; } = null!;

        /// <summary>رقم قسيمة التسوية والمهمة</summary>
        public string AdjustmentNumber { get; set; } = null!;
        
        /// <summary>طبيعة التسوية (جرد، إتلاف ناتج عن كسر، رصيد افتتاحي لمنشأة جديدة)</summary>
        public AdjustmentType AdjustmentType { get; set; }
        
        /// <summary>حالة اعتماد المحاسب لوثيقة الجرد ليتم تحريك المخزون وإضافة القيد المالي</summary>
        public AdjustmentStatus Status { get; set; } = AdjustmentStatus.Draft;

        /// <summary>المستخدم أو المدير المخول بالموافقة على التسوية</summary>
        public Guid? ApprovedByUserId { get; set; }
        
        /// <summary>زمن الاعتماد الفوري والحقيقي</summary>
        public DateTime? ApprovedAt { get; set; }

        /// <summary>زمن طلب فتح وإجراء التسوية</summary>
        public DateTime AdjustedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>ملاحظات عامة لسبب العجز المكتشف</summary>
        public string? Notes { get; set; }

        /// <summary>القيد المحاسبي المولد من التسوية لإثبات تكلفة البضاعة المباعة أو التالف في الميزانية</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>تفاصيل البضائع المسواة والمجردة من حيث التلف والزيادة والنقصان</summary>
        public virtual ICollection<InventoryAdjustmentLine> Lines { get; set; } = new List<InventoryAdjustmentLine>();
    }
}
