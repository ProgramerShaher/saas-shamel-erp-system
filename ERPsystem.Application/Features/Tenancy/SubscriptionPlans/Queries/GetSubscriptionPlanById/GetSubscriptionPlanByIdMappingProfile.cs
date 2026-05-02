using AutoMapper;
using ERPsystem.Domain.Entities.Tenancy;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    /// <summary>
    /// تعيين الداتا لباقة واحدة.
    /// </summary>
    public class GetSubscriptionPlanByIdMappingProfile : Profile
    {
        public GetSubscriptionPlanByIdMappingProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanDto>();
        }
    }
}
