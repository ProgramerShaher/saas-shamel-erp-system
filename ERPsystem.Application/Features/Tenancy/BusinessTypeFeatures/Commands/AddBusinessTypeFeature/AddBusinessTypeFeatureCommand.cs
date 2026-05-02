using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.AddBusinessTypeFeature
{
    /// <summary>
    /// أمر إضافة ميزة واحدة محددة لنوع نشاط تجاري.
    /// </summary>
    public class AddBusinessTypeFeatureCommand : IRequest<BaseResponse<Guid>>
    {
        public BusinessType BusinessType { get; set; }
        public FeatureKey FeatureKey { get; set; }
    }
}
