using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل تصنيف للمنتجات (مثال: ألبان، منظفات) ويدعم الشجرة الهرمية.
    /// </summary>
    public class Category : TenantBaseEntity
    {
        /// <summary>اسم التصنيف</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>وصف التصنيف</summary>
        public string? Description { get; set; }
        
        /// <summary>معرف التصنيف الأب في حال كان هذا التصنيف فرعياً</summary>
        public Guid? ParentCategoryId { get; set; }

        // Navigation
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
