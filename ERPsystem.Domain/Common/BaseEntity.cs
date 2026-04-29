using System;

namespace ERPsystem.Domain.Common
{
    /// <summary>
    /// الكيان الأساسي الذي ترث منه جميع الكيانات في النظام.
    /// يحتوي على الخصائص المشتركة لأي جدولمثل المعرف والتتبع.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>المعرف الفريد للكائن</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>تاريخ إنشاء السجل</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>معرف المستخدم الذي قام بإنشاء السجل</summary>
        public Guid CreatedBy { get; set; }

        /// <summary>تاريخ آخر تعديل للسجل</summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>معرف المستخدم الذي قام بآخر تعديل</summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>حالة الحذف المنطقي السجل (Soft Delete). إذا كانت صحيحة، فالسجل محذوف.</summary>
        public bool IsDeleted { get; set; } = false;
    }
}
