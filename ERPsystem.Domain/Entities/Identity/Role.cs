using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل مجموعة من الصلاحيات والأدوار (Role) كمدير، محاسب وهكذا.
    /// </summary>
    public class Role : TenantBaseEntity
    {
        /// <summary>اسم الدور (مثال: Manager, Cashier)</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>وصف الدور</summary>
        public string? Description { get; set; }

        // Navigation
        public virtual ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
