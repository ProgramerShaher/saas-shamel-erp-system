using System;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById
{
    /// <summary>
    /// يتم استخدام هذا الـ Data Transfer Object لنقل بيانات المنشأة المطلوبة.
    /// تم اتباع التسمية Dto بحسب التعليمات.
    /// </summary>
    public class TenantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public BusinessType BusinessType { get; set; }
        public bool IsActive { get; set; }
        public string? LogoUrl { get; set; }
        public string? PrimaryColor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
