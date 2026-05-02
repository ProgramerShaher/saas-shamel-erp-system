using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Commands.UpsertTenantFeatureFlag
{
    /// <summary>
    /// أمر تفعيل/إلغاء تفعيل وتحديث ميزة خاصة بمنشأة معينة.
    /// يتم استخدام Upsert بحيث يقوم بإنشائها إن لم تكن موجودة أو تحديثها إذا وُجدت.
    /// </summary>
    public class UpsertTenantFeatureFlagCommand : IRequest<BaseResponse<Guid>>
    {
        public Guid TenantId { get; set; }
        public string FeatureKey { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public string? ConfigJson { get; set; }
        public Guid? EnabledByUserId { get; set; }
    }
}
