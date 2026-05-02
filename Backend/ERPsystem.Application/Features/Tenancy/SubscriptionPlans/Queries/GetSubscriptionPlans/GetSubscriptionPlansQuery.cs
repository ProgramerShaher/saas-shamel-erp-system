using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans
{
    /// <summary>
    /// استعلام جلب قائمة باقات الاشتراك.
    /// </summary>
    public class GetSubscriptionPlansQuery : IRequest<PagedResponse<SubscriptionPlanDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchKeyword { get; set; }
    }
}
