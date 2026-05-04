using Microsoft.AspNetCore.Identity;
using System;

namespace ERPsystem.Infrastructure.Identity
{
    /// <summary>
    /// الدور المخصص لنظام ERP، يرث من Microsoft IdentityRole مع إمكانية إضافة حقول إضافية.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>اسم الدور أو المجموعة الوظيفية المفهومة للعميل</summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>وصف الدور وأبرز مسؤولياته</summary>
        public string? Description { get; set; }

        /// <summary>هل هذا الدور هو دور افتراضي من النظام لا يمكن كسر أدواته أو حذفه بالكامل؟</summary>
        public bool IsSystemRole { get; set; } = false;

        // ملاحظة: خصائص التنقل (Navigation Properties) مثل UserRoles, RolePermissions
        // لا تضاف هنا مباشرة في IdentityRole، بل تضاف في DbContext أو عبر علاقات منفصلة إذا لزم الأمر.
    }
}
