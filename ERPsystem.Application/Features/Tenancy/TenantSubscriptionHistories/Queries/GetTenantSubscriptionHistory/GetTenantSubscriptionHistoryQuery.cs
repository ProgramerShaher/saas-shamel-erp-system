using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory
{
    /// <summary>
    /// استعلام جلب تاريخ الاشتراكات الخاصة بمنشأة محددة.
    /// </summary>
    public class GetTenantSubscriptionHistoryQuery : IRequest<BaseResponse<System.Collections.Generic.List<TenantSubscriptionHistoryDto>>>
    {
        public Guid TenantId { get; set; }
    }
}
