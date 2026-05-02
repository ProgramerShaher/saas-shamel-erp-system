using System.Collections.Generic;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetAllBusinessTypeFeatures
{
    /// <summary>
    /// طلب جلب جميع قوالب الميزات لجميع أنواع الأنشطة التجارية.
    /// </summary>
    public class GetAllBusinessTypeFeaturesQuery : IRequest<BaseResponse<List<AllBusinessTypeFeaturesDto>>>
    {
    }

    public class AllBusinessTypeFeaturesDto
    {
        public BusinessType BusinessType { get; set; }
        public string BusinessTypeName { get; set; } = null!;
        public string FeatureKey { get; set; } = null!;
        public bool IsEnabledByDefault { get; set; }
    }
}
