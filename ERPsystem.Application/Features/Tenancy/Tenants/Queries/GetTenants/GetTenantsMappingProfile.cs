using AutoMapper;
using ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants
{
    /// <summary>
    /// ملف الـ Mapping الخاص بميزة إدارة المنشآت.
    /// يُطبق قاعدة أولاً من الدستور:
    ///   - اتجاه الـ Mapping من Entity → ViewModel فقط.
    ///   - يمنع استخدام AutoMapper من Command → Entity (يتم يدوياً في الـ Handler).
    ///   - يُتجاهل أي حقل حساس (كـ PasswordHash في المستخدمين).
    ///   - كل Feature تملك Profile مستقل خاص بها.
    /// </summary>
    public class GetTenantsMappingProfile : Profile
    {
        public GetTenantsMappingProfile()
        {
            // ══════════════════════════════════════════════════════════
            //  Tenant Entity → TenantDto (القائمة والبحث)
            //  يُستخدم هذا الـ Mapping مع ProjectTo في الـ QueryHandler
            //  ليتحول مباشرة داخل SQL دون جلب كل الأعمدة للذاكرة.
            // ══════════════════════════════════════════════════════════
            CreateMap<Tenant, TenantDto>()
                .ForMember(dest => dest.Id,           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,         opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug,         opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.BusinessType))
                .ForMember(dest => dest.IsActive,     opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.LogoUrl,      opt => opt.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.CreatedAt,    opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
