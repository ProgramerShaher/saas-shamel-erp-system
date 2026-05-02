using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Organization;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// المستودع: مكان تجميع ومراقبة تحركات العناصر والمخزون.
    /// </summary>
    public class Warehouse : TenantBaseEntity
    {
        /// <summary>معرف الفرع التابع له المستودع</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع</summary>
        public virtual Branch Branch { get; set; } = null!;

        /// <summary>اسم المستودع</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>الرمز المختصر للمستودع (مثال: WH01)</summary>
        public string? Code { get; set; }
        
        /// <summary>نوع المستودع (شامل، عرض، تبريد، بضاعة تالفة)</summary>
        public WarehouseType WarehouseType { get; set; }
        
        /// <summary>وضع المستودع للعمليات المخزنية (استلام وصرف)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>معرف المستخدم أمين المستودع</summary>
        public Guid? ManagerUserId { get; set; }
    }
}
