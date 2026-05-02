using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.RemoveBusinessTypeFeature
{
    /// <summary>
    /// أمر حذف ميزة واحدة محددة من نشاط تجاري.
    /// </summary>
    public class RemoveBusinessTypeFeatureCommand : IRequest<BaseResponse<bool>>
    {
        public BusinessType BusinessType { get; set; }
        public FeatureKey FeatureKey { get; set; }
    }
}
