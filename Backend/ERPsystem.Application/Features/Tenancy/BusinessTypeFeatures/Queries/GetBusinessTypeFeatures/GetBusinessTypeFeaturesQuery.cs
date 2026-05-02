using System.Collections.Generic;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetBusinessTypeFeatures
{
    /// <summary>
    /// طلب جلب قائمة الميزات المخصصة لنوع نشاط تجاري معين.
    /// </summary>
    public class GetBusinessTypeFeaturesQuery : IRequest<BaseResponse<List<BusinessTypeFeatureDto>>>
    {
        public BusinessType BusinessType { get; set; }
    }
}
