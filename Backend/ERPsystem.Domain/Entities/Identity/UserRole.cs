using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Identity
{
    
    /// <summary>
    /// جدول الوسيط لربط المستخدم بالدور.
    /// يدعم التخصيص المكاني؛ فالمستخدم قد يكون (مدير) في فرع أ، و(كاشير) في فرع ب.
    /// </summary>
    public class UserRole : TenantBaseEntity
    {
        /// <summary>معرف المستخدم</summary>
        public Guid UserId { get; set; }
        
        /// <summary>المستخدم</summary>
        public virtual User User { get; set; } = null!;

        /// <summary>معرف الدور (مجموعة الصلاحيات)</summary>
        public Guid RoleId { get; set; }
        
        /// <summary>الدور الممنوح</summary>
        public virtual Role Role { get; set; } = null!;

        /// <summary>الفرع المسموح به لهذه الصلاحية (إذا كان Null، فالمستخدم له هذا الدور على جميع الفروع بمنشأته)</summary>
        public Guid? BranchId { get; set; }
        
        /// <summary>الفرع المعمول به للمنح</summary>
        public virtual Organization.Branch? Branch { get; set; }

        /// <summary>تاريخ ووقت منح هذه الصلاحية</summary>
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>معرف المستخدم الذي منح الصلاحية (Manager)</summary>
        public Guid AssignedByUserId { get; set; }
    }
}
