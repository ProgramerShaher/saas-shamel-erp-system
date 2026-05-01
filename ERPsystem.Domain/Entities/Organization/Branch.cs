using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Organization;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// الفرع المحاسبي والإداري. الوحدة التشغيلية التي يتم تحويل البضاعة لها وتحرير مبيعاتها.
    /// </summary>
    public class Branch : TenantBaseEntity
    {
        /// <summary>معرف المقر الرئيسي</summary>
        public Guid HeadquartersId { get; set; }
        
        /// <summary>المقر التابع له هذا الفرع</summary>
        public virtual Headquarters Headquarters { get; set; } = null!;

        /// <summary>الرمز التعريفي المختصر للفرع لتوليد البادئات التسلسلية</summary>
        public string Code { get; set; } = null!;
        
        /// <summary>اسم الفرع المتعارف عليه</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>طبيعة عمل الفرع (رئيسي، معرض، كشك)</summary>
        public BranchType BranchType { get; set; }

        /// <summary>عنوان الفرع الفعلي</summary>
        public string? Address { get; set; }
        
        /// <summary>رقم الهاتف الخاص بالفرع</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>المنطقة الزمنية الخاصة لضبط تقفيل الورديات</summary>
        public string? TimeZone { get; set; }
        
        /// <summary>هل الفرع شغال أم مغلق تعسفيا؟</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>مركز التكلفة المرتبط لجمع مصروفات هذا الفرع في الشجرة المحاسبية</summary>
        public Guid? CostCenterId { get; set; }
        
        /// <summary>معرف مستخدم المدير المسؤول</summary>
        public Guid? ManagerUserId { get; set; }

        // Navigation Properties
        /// <summary>مستودعات الفرع</summary>
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        
        /// <summary>أجهزة الكاشير في الفرع</summary>
        public virtual ICollection<PosTerminal> PosTerminals { get; set; } = new List<PosTerminal>();
        
        /// <summary>خزائن الكاشير والصناديق الرئيسية للفرع</summary>
        public virtual ICollection<CashSafe> CashSafes { get; set; } = new List<CashSafe>();
    }
}
