using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Domain.Entities.Tenancy
{
    /// <summary>
    /// المنشأة/المستأجر — الجذر الأعلى في هيكل النظام.
    /// كل بيانات النظام تنتمي لـ Tenant معين.
    /// </summary>
    public class Tenant : BaseEntity
    {
        /// <summary>اسم المنشأة التجارية أو الشركة</summary>
        public string Name { get; set; } = null!;

        /// <summary>النطاق الفرعي المميز للمنشأة (مثال: alnahdi.erp.com)</summary>
        public string Slug { get; set; } = null!;

        /// <summary>نوع النشاط التجاري لتحديد شكل النظام (صيدلية، سوبرماركت، ملابس)</summary>
        public BusinessType BusinessType { get; set; }

        /// <summary>معرف باقة الاشتراك الحالية للمنشأة</summary>
        public Guid SubscriptionPlanId { get; set; }
        
        /// <summary>باقة الاشتراك المربوطة بهذه المنشأة</summary>
        public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        /// <summary>حالة الحساب (نشط أم موقوف)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>اسم الـ Schema الخاص بهذه المنشأة في قاعدة البيانات لتأمين العزل</summary>
        public string DatabaseSchema { get; set; } = null!;

        /// <summary>رابط لوجو المنشأة لطباعته في الفواتير والواجهة</summary>
        public string? LogoUrl { get; set; }
        
        /// <summary>اللون الأساسي لواجهة الموظفين الخاصة بالمنشأة</summary>
        public string? PrimaryColor { get; set; }

        // Navigation Properties
        /// <summary>قائمة الميزات والخصائص المفعلة لهذه المنشأة</summary>
        public virtual ICollection<TenantFeatureFlag> FeatureFlags { get; set; } = new List<TenantFeatureFlag>();
        
        /// <summary>سجل تاريخ تغيير وتجديد الاشتراكات لهذه المنشأة</summary>
        public virtual ICollection<TenantSubscriptionHistory> SubscriptionHistory { get; set; } = new List<TenantSubscriptionHistory>();
    }
}
