using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    /// <summary>
    /// استعلام جلب باقة واحدة.
    /// </summary>
    public class GetSubscriptionPlanByIdQuery : IRequest<BaseResponse<SubscriptionPlanDto>>
    {
        public Guid Id { get; set; }
    }
}
