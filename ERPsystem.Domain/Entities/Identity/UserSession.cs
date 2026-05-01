using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Identity
{
    /// <summary>
    /// جلسات تسجيل الدخول والتوظيف النشطة للمستخدمين للتحكم في (Refresh Tokens) للـ API والجوال.
    /// </summary>
    public class UserSession : TenantBaseEntity
    {
        /// <summary>معرف المستخدم</summary>
        public Guid UserId { get; set; }
        
        /// <summary>المستخدم الدارج</summary>
        public virtual User User { get; set; } = null!;

        /// <summary>رمز استعادة تسجيل الدخول دون باسوورد للتوكن المنتهي (JWT)</summary>
        public string RefreshToken { get; set; } = null!;
        
        /// <summary>معلومات الجهاز/المتصفح الذي تم منه فتح الدخول لمراقبة الاختراق</summary>
        public string? DeviceInfo { get; set; }
        
        /// <summary>رقم اتصال الشبكة عند تسجيل الدخول (IP Address)</summary>
        public string? IpAddress { get; set; }

        /// <summary>تاريخ انقضاء الجلسة إجباريا</summary>
        public DateTime ExpiresAt { get; set; }
        
        /// <summary>تاريخ إلغاء الجلسة بالقوة (تسجيل الخروج من كل الأجهزة)</summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>مؤشر على فعالية الجلسة حاليا</summary>
        public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
    }
}
