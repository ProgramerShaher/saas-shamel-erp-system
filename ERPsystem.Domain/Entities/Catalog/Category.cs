using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// شجرة تصنيف المنتجات أو شجرة الأصناف (هيكل هرمي لا نهائي التفرع).
    /// مثال: إلكترونيات > هواتف ذكية > آبل.
    /// </summary>
    public class Category : TenantBaseEntity
    {
        /// <summary>معرف التصنيف الأب (إذا كان هذا التصنيف فرعياً)</summary>
        public Guid? ParentCategoryId { get; set; }
        
        /// <summary>التصنيف الأب</summary>
        public virtual Category? ParentCategory { get; set; }

        /// <summary>اسم التصنيف (ألبان وأجبان، إلكترونيات، مقبلات)</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>رمز أو باركود للتصنيف لسهولة الاستدعاء</summary>
        public string? Code { get; set; }
        
        /// <summary>مستوى الحساب في شجرة المجموعات (من 1 فأكثر)</summary>
        public int Level { get; set; } = 0;
        
        /// <summary>الرمز الرقمي المخصص لترتيب ظهور الفئات في نقاط البيع (POS)</summary>
        public int DisplayOrder { get; set; } = 0;
        
        /// <summary>حالة العمل للمجموعة (إذا توقفت فلن تظهر أصنافها للبيع)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>التصنيفات الفرعية المتشعبة من هذا التصنيف</summary>
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        
        /// <summary>جميع الأصناف المدرجة أسفل هذا التصنيف</summary>
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
