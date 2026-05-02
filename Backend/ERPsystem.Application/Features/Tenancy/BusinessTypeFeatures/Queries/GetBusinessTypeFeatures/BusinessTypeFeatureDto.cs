using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetBusinessTypeFeatures
{
    /// <summary>
    /// كائن نقل البيانات الخاص بميزات نوع النشاط التجاري.
    /// </summary>
    public class BusinessTypeFeatureDto
    {
        /// <summary>المفتاح البرمجي للميزة</summary>
        public string FeatureKey { get; set; } = null!;

        /// <summary>اسم الميزة (للعرض)</summary>
        public string FeatureName { get; set; } = null!;

        /// <summary>هل هي مفعلة افتراضياً لهذا النشاط؟</summary>
        public bool IsEnabledByDefault { get; set; }
    }
}
