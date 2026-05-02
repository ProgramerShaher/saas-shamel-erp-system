using AutoMapper;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans
{
    /// <summary>
    /// تعيين بيانات باقة الاشتراك للقائمة حصرياً.
    /// </summary>
    public class GetSubscriptionPlansMappingProfile : Profile
    {
        public GetSubscriptionPlansMappingProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanDto>();
        }
    }
}
