using AutoMapper;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags
{
    /// <summary>
    /// ملف التحويل الخاص بميزات المنشأة. مُضَمّن داخل الميزة كما ينص الدستور.
    /// </summary>
    public class GetTenantFeatureFlagsMappingProfile : Profile
    {
        public GetTenantFeatureFlagsMappingProfile()
        {
            CreateMap<TenantFeatureFlag, TenantFeatureFlagDto>();
        }
    }
}
