using System;
using MediatR;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.CreateTenant
{
    /// <summary>
    /// أمر إنشاء منشأة جديدة (Tenant) في النظام.
    /// يُستخدم هذا الكائن كـ DTO لاستقبال بيانات المنشأة من طبقة الـ API.
    /// يُطبق مبدأ CQRS: هذا Command يغير حالة قاعدة البيانات.
    /// يُرجع BaseResponse يحتوي على معرف المنشأة الجديدة (Guid).
    /// </summary>
    public record CreateTenantCommand : IRequest<BaseResponse<Guid>>
    {
        /// <summary>الاسم الرسمي للشركة أو المنشأة التجارية</summary>
        public string Name { get; init; } = null!;

        /// <summary>النطاق الفرعي المميز (يجب أن يكون فريداً)</summary>
        public string Slug { get; init; } = null!;

        /// <summary>نوع النشاط التجاري (ماركت، صيدلية، مول، إلخ)</summary>
        public BusinessType BusinessType { get; init; }

        /// <summary>معرف باقة الاشتراك المختارة</summary>
        public Guid SubscriptionPlanId { get; init; }

        /// <summary>اللون الأساسي للواجهة (اختياري)</summary>
        public string? PrimaryColor { get; init; }

        /// <summary>رابط اللوجو (اختياري)</summary>
        public string? LogoUrl { get; init; }
    }
}
