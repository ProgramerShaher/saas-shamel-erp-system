using AutoMapper;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory
{
    /// <summary>
    /// ملف التحويل لبيانات السجل التاريخي للاشتراك.
    /// </summary>
    public class GetTenantSubscriptionHistoryMappingProfile : Profile
    {
        public GetTenantSubscriptionHistoryMappingProfile()
        {
            CreateMap<TenantSubscriptionHistory, TenantSubscriptionHistoryDto>();
        }
    }
}
