using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags
{
    /// <summary>
    /// استعلام جلب ميزات منشأة محددة.
    /// </summary>
    public class GetTenantFeatureFlagsQuery : IRequest<BaseResponse<System.Collections.Generic.List<TenantFeatureFlagDto>>>
    {
        public Guid TenantId { get; set; }
    }
}
