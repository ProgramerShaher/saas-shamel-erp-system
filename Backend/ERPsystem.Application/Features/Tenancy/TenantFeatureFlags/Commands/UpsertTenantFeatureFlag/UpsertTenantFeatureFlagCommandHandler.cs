using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Commands.UpsertTenantFeatureFlag
{
    /// <summary>
    /// معالج أمر إنشاء أو تحديث ميزة منشأة.
    /// </summary>
    public class UpsertTenantFeatureFlagCommandHandler : IRequestHandler<UpsertTenantFeatureFlagCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpsertTenantFeatureFlagCommandHandler> _logger;

        public UpsertTenantFeatureFlagCommandHandler(
            IApplicationDbContext context,
            ILogger<UpsertTenantFeatureFlagCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(UpsertTenantFeatureFlagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ تفعيل/تحديث ميزة [{FeatureKey}] للمنشأة [{TenantId}]", request.FeatureKey, request.TenantId);

            var feature = await _context.TenantFeatureFlags
                .FirstOrDefaultAsync(f => f.TenantId == request.TenantId && f.FeatureKey == request.FeatureKey, cancellationToken);

            if (feature == null)
            {
                feature = new TenantFeatureFlag
                {
                    TenantId = request.TenantId,
                    FeatureKey = request.FeatureKey,
                    IsEnabled = request.IsEnabled,
                    ConfigJson = request.ConfigJson,
                    EnabledAt = request.IsEnabled ? DateTime.UtcNow : null,
                    EnabledByUserId = request.EnabledByUserId
                };
                await _context.TenantFeatureFlags.AddAsync(feature, cancellationToken);
            }
            else
            {
                feature.IsEnabled = request.IsEnabled;
                feature.ConfigJson = request.ConfigJson;
                if (request.IsEnabled)
                {
                    feature.EnabledAt = DateTime.UtcNow;
                    feature.EnabledByUserId = request.EnabledByUserId ?? feature.EnabledByUserId;
                }
                _context.TenantFeatureFlags.Update(feature);
            }

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ بنجاح تم حفظ حالة الميزة | المعرف: [{FeatureId}]", feature.Id);
            return new BaseResponse<Guid>(feature.Id, "تم حفظ حالة الميزة بنجاح.");
        }
    }
}
