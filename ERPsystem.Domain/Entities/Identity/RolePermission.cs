using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Identity
{
    /// <summary>
    /// ربط الدور بالمفاتيح الصلاحية المحددة.
    /// يعبر عن (الدور الفلاني يمتلك الصلاحية الفلانية وتم التمكين).
    /// </summary>
    public class RolePermission : BaseEntity
    {
        /// <summary>معرف الدور المخصص</summary>
        public Guid RoleId { get; set; }
        
        /// <summary>الدور</summary>
        public virtual Role Role { get; set; } = null!;

        /// <summary>معرف صلاحية النظام (من الجدول العام)</summary>
        public Guid PermissionId { get; set; }
        
        /// <summary>الصلاحية الممنوحة</summary>
        public virtual Permission Permission { get; set; } = null!;

        /// <summary>هل الصلاحية ممنوحة أم مرفوضة صراحة؟</summary>
        public bool IsGranted { get; set; } = true;
    }
}
