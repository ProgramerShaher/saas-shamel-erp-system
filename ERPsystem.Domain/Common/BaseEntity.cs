using System;

namespace ERPsystem.Domain.Common
{
    /// <summary>
    /// الكيان الجذر الذي ترث منه جميع كيانات النظام بدون استثناء.
    /// يحتوي على المعرف، تتبع الإنشاء والتعديل، والحذف المنطقي.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>المعرف الفريد للكيان (Primary Key)</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>تاريخ ووقت إنشاء السجل في قاعدة البيانات</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>معرف المستخدم الذي قام بإنشاء هذا السجل</summary>
        public Guid CreatedBy { get; set; }

        /// <summary>تاريخ ووقت آخر تعديل على السجل</summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>معرف المستخدم الذي قام بآخر تعديل</summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>مؤشر الحذف المنطقي (Soft Delete)، true تعني أن السجل محذوف ولا يظهر للمستخدم</summary>
        public bool IsDeleted { get; set; } = false;
        
        /// <summary>تاريخ ووقت عملية الحذف المنطقي</summary>
        public DateTime? DeletedAt { get; set; }
        
        /// <summary>معرف المستخدم الذي قام بعملية الحذف</summary>
        public Guid? DeletedBy { get; set; }
    }
}
