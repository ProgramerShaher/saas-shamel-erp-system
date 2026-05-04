using System;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants
{
    /// <summary>
    /// نموذج البيانات (DTO) لاسترجاع بيانات المنشأة في القوائم.
    /// يستخدم في الـ ProjectTo (Projection) مباشرة من قاعدة البيانات
    /// لمنع جلب بيانات حساسة أو Navigation Properties زائدة.
    /// </summary>
    public class TenantDto
    {
        public Guid Id { 
            get; set; }

        /// <summary>اسم المنشأة</summary>
        public string Name { get; set; } = null!;

        /// <summary>النطاق الفرعي</summary>
        public string Slug { get; set; } = null!;

        /// <summary>نوع النشاط التجاري</summary>
        public BusinessType BusinessType { get; set; }

        /// <summary>هل المنشأة نشطة؟</summary>
        public bool IsActive { get; set; }

        /// <summary>رابط اللوجو</summary>
        public string? LogoUrl { get; set; }

        /// <summary>تاريخ الإنشاء</summary>
        public DateTime CreatedAt { get; set; }
    }
}
