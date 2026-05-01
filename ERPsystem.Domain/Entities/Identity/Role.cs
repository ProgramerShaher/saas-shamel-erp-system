using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Identity
{
    /// <summary>
    /// الدور الوظيفي للإدارة والصلاحيات — يمثل مجموعة من الصلاحيات تُعطى للمستخدمين.
    /// أمثلة: (مدير الحسابات، مشرف الفروع، كاشير مبيعات، أمين مستودع).
    /// </summary>
    public class Role : TenantBaseEntity
    {
        /// <summary>اسم الدور أو المجموعة الوظيفية المفهومة للعميل</summary>
        public string RoleName { get; set; } = null!;
        
        /// <summary>وصف الدور وأبرز مسؤولياته</summary>
        public string? Description { get; set; }

        /// <summary>هل هذا الدور هو دور افتراضي من النظام لا يمكن كسر أدواته أو حذفه بالكامل؟</summary>
        public bool IsSystemRole { get; set; } = false;

        // Navigation Properties
        /// <summary>المستخدمين الذين يحملون هذا الدور</summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        
        /// <summary>علاقات هذا الدور والمفاتيح المسموح بفتحها (الصلاحيات)</summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
