using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Identity
{
    /// <summary>
    /// تعريف مفتاح الصلاحية النهائي والأصيل— هذه الجداول عالمية وثابتة (ليست لكل منشأة)
    /// تُعرف كافة الأفعال في النظام (مثال إنشاء فاتورة، طباعة تقرير، السماح بجرد).
    /// </summary>
    public class Permission : BaseEntity
    {
        /// <summary>اسم الوحدة الرئيسية التي تتبع لها الصلاحية — مثال: المبيعات، المخازن، المالية</summary>
        public string ModuleName { get; set; } = null!;

        /// <summary>مفتاح الصلاحية الفريد باللغة الإنجليزية — مثال: Sales.Invoice.Create</summary>
        public string PermissionKey { get; set; } = null!;

        /// <summary>وصف الصلاحية (ما الذي سيفتح إذا امتلكها المستخدم؟)</summary>
        public string? Description { get; set; }

        /// <summary>الأدوار التي تحمل هذا المفتاح</summary>
        public System.Collections.Generic.ICollection<RolePermission> RolePermissions { get; set; }
            = new System.Collections.Generic.List<RolePermission>();
    }
}
