using System.Collections.Generic;
using ERPsystem.Domain.Entities.Tenancy;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Common.Interfaces
{
    /// <summary>
    /// خدمة إعداد المنشآت الجديدة. مسؤولة عن توليد الإعدادات الافتراضية
    /// والصلاحيات والميزات (Feature Flags) الخاصة بكل منشأة بناءً على نشاطها التجاري.
    /// </summary>
    public interface ITenantSetupService
    {
        /// <summary>
        /// توليد قائمة الميزات الافتراضية التي يجب تفعيلها للمنشأة الجديدة
        /// بناءً على نوع نشاطها التجاري (سوبر ماركت، صيدلية، الخ).
        /// </summary>
        /// <param name="businessType">نوع النشاط التجاري</param>
        /// <returns>قائمة بكيانات أعلام الميزات (Feature Flags)</returns>
        List<TenantFeatureFlag> GenerateDefaultFeatureFlags(BusinessType businessType);
    }
}
