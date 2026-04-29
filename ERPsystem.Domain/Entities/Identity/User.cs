using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل المستخدم الفردي (كاشير، محاسب، مدير) التابع لمنشأة معينة.
    /// </summary>
    public class User : TenantBaseEntity
    {
        /// <summary>الاسم الكامل للمستخدم</summary>
        public string FullName { get; set; } = string.Empty;
        
        /// <summary>البريد الإلكتروني ويستخدم في تسجيل الدخول</summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>تجزئة كلمة المرور المشفرة (Hash)</summary>
        public string PasswordHash { get; set; } = string.Empty;
        
        /// <summary>رقم الجوال الخاص بالمستخدم</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>حالة نشاط حساب المستخدم</summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>معرف الدور (الصلاحيات) المرتبط بهذا المستخدم</summary>
        public Guid RoleId { get; set; }

        // Navigation
        public virtual Role Role { get; set; } = null!;
    }
}
