using AutoMapper;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById
{
    /// <summary>
    /// ملف الـ Mapping الخاص بميزة جلب منشأة بواسطة الـ Id.
    /// يُطبق قاعدة الـ Mapping الخاص بكل Feature.
    /// </summary>
    public class GetTenantByIdMappingProfile : Profile
    {
        public GetTenantByIdMappingProfile()
        {
            CreateMap<Tenant, TenantDto>()
                .ForMember(dest => dest.Id,           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,         opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug,         opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.BusinessType))
                .ForMember(dest => dest.IsActive,     opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.LogoUrl,      opt => opt.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.PrimaryColor, opt => opt.MapFrom(src => src.PrimaryColor))
                .ForMember(dest => dest.CreatedAt,    opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
