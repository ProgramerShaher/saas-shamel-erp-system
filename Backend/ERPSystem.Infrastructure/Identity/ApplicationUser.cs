using Microsoft.AspNetCore.Identity;
using System;

namespace ERPsystem.Infrastructure.Identity
{
    /// <summary>
    /// المستخدم الخاص بنظام ERP، يرث من Microsoft IdentityUser مع حقول إضافية مخصصة.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>الاسم الكامل للمستخدم (يظهر في الفواتير والتقارير)</summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>نوع دور المستخدم الأساسي كمدير أو كاشير</summary>
        public int UserType { get; set; } // استخدم int بدلاً من enum مباشرة لدعم EF Identity

        /// <summary>حالة العمل للمستخدم (موظف نشط أو مقال)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>معرف الفرع الافتراضي المعين عليه هذا الموظف</summary>
        public Guid DefaultBranchId { get; set; }

        /// <summary>رابط صورة المستخدم الشخصية</summary>
        public string? AvatarUrl { get; set; }

        /// <summary>نقطة البيع (الكاشير) المخصصة لهذا المستخدم وتكون Null للإداريين</summary>
        public Guid? DefaultPosTerminalId { get; set; }

        /// <summary>تاريخ ووقت آخر عملية تسجيل دخول للمستخدم في النظام</summary>
        public DateTime? LastLoginAt { get; set; }

        // ملاحظة: خصائص التنقل (Navigation Properties) مثل DefaultBranch, DefaultPosTerminal, UserRoles, Sessions
        // لا تضاف هنا مباشرة في IdentityUser، بل تضاف في DbContext أو عبر علاقات منفصلة إذا لزم الأمر.
    }
}
