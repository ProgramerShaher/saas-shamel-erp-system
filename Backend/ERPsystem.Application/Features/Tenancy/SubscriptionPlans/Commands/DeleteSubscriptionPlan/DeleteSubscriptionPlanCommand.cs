using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.DeleteSubscriptionPlan
{
    /// <summary>
    /// أمر حذف/إيقاف باقة التمويل.
    /// </summary>
    public class DeleteSubscriptionPlanCommand : IRequest<BaseResponse<Guid>>
    {
        public Guid Id { get; set; }
    }
}
