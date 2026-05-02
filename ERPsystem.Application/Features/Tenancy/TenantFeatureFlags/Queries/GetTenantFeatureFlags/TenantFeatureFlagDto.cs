using System;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags
{
    /// <summary>
    /// الكائن الناقل لبيانات الميزة.
    /// </summary>
    public class TenantFeatureFlagDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string FeatureKey { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public string? ConfigJson { get; set; }
        public DateTime? EnabledAt { get; set; }
    }
}
