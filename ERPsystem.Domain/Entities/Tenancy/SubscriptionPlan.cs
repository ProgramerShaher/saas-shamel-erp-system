using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Tenancy
{
    /// <summary>
    /// باقات الاشتراك المتاحة (Starter / Pro / Enterprise).
    /// تحدد الحدود القصوى والوحدات المسموح بها لكل منشأة.
    /// </summary>
    public class SubscriptionPlan : BaseEntity
    {
        /// <summary>اسم الباقة التجارية (مثال: الباقة الذهبية)</summary>
        public string PlanName { get; set; } = null!;
        
        /// <summary>وصف مميزات الباقة</summary>
        public string? Description { get; set; }

        /// <summary>سعر الاشتراك الشهري</summary>
        public decimal MonthlyPrice { get; set; }
        
        /// <summary>سعر الاشتراك السنوي (اختياري)</summary>
        public decimal? AnnualPrice { get; set; }

        /// <summary>الحد الأقصى المسموح به لعدد الفروع</summary>
        public int MaxBranches { get; set; }
        
        /// <summary>الحد الأقصى لعدد نقاط البيع (الكاشيرات) المسموحة</summary>
        public int MaxPosTerminals { get; set; }
        
        /// <summary>الحد الأقصى لعدد المستخدمين المسموح لهم الدخول</summary>
        public int MaxUsers { get; set; }
        
        /// <summary>الحد الأقصى لعدد الأصناف الممكن تسجيلها في النظام</summary>
        public int MaxItems { get; set; }

        /// <summary>الوحدات المفعلة لهذه الباقة في شكل JSON (مثال: ["POS", "Inventory"])</summary>
        public string AllowedModulesJson { get; set; } = "[]";

        /// <summary>حالة الباقة (هل لا تزال معروضة للبيع للعملاء؟)</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>قائمة المنشآت المشتركة في هذه الباقة</summary>
        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    }
}
