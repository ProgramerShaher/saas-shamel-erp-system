using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Identity;

namespace ERPsystem.Domain.Entities.Identity
{
    /// <summary>
    /// مستخدم النظام (كاشير، مدير، محاسب، موظف مبيعات).
    /// </summary>
    public class User : TenantBaseEntity
    {
        /// <summary>اسم المستخدم للولوج (غالبا باللغة الإنجليزية)</summary>
        public string Username { get; set; } = null!;
        
        /// <summary>البريد الإلكتروني للولوج والتنبيهات</summary>
        public string Email { get; set; } = null!;
        
        /// <summary>تجزئة كلمة المرور المشفرة (Hash)</summary>
        public string PasswordHash { get; set; } = null!;
        
        /// <summary>الاسم الكامل للمستخدم (يظهر في الفواتير والتقارير)</summary>
        public string FullName { get; set; } = null!;
        
        /// <summary>رقم هاتف المستخدم للتواصل الإداري</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>رابط صورة المستخدم الشخصية</summary>
        public string? AvatarUrl { get; set; }

        /// <summary>نوع دور المستخدم الأساسي كمدير أو كاشير</summary>
        public UserType UserType { get; set; }
        
        /// <summary>حالة العمل للمستخدم (موظف نشط أو مقال)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>الفرع الافتراضي المعين عليه هذا الموظف لممارسة عمله وصلاحياته</summary>
        public Guid DefaultBranchId { get; set; }
        
        /// <summary>الفرع الافتراضي</summary>
        public virtual Organization.Branch DefaultBranch { get; set; } = null!;

        /// <summary>نقطة البيع (الكاشير) المخصصة لهذا المستخدم وتكون Null للإداريين</summary>
        public Guid? DefaultPosTerminalId { get; set; }
        
        /// <summary>جهاز الكاشير الافتراضي</summary>
        public virtual Organization.PosTerminal? DefaultPosTerminal { get; set; }

        /// <summary>تاريخ ووقت آخر عملية تسجيل دخول للمستخدم في النظام</summary>
        public DateTime? LastLoginAt { get; set; }

        // Navigation Properties
        /// <summary>الأدوار (مجموعات الصلاحيات) الموزعة على هذا المستخدم</summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        
        /// <summary>جلسات استخدام النظام المفتوحة عبر المفاتيح الآمنة</summary>
        public virtual ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
    }
}
