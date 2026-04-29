using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يحدد صلاحيات دور معين عبر تقسيمات النظام (الوحدات).
    /// </summary>
    public class RolePermission : TenantBaseEntity
    {
        /// <summary>متصل بالدور المعني بهذه الصلاحيات</summary>
        public Guid RoleId { get; set; }
        
        /// <summary>اسم الوحدة (مثال: المبيعات، المخزون، الحسابات)</summary>
        public string Module { get; set; } = string.Empty;
        
        /// <summary>صلاحية العرض أو القراءة</summary>
        public bool CanView { get; set; }
        
        /// <summary>صلاحية إنشاء بيانات جديدة</summary>
        public bool CanCreate { get; set; }
        
        /// <summary>صلاحية التعديل</summary>
        public bool CanUpdate { get; set; }
        
        /// <summary>صلاحية الحذف</summary>
        public bool CanDelete { get; set; }

        // Navigation
        public virtual Role Role { get; set; } = null!;
    }
}
