using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Commands.UpsertTenantFeatureFlag
{
    /// <summary>
    /// مدقق مدخلات تفعيل ميزة لمنشأة.
    /// </summary>
    public class UpsertTenantFeatureFlagCommandValidator : AbstractValidator<UpsertTenantFeatureFlagCommand>
    {
        public UpsertTenantFeatureFlagCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");
            RuleFor(x => x.FeatureKey).NotEmpty().WithMessage("مفتاح الميزة مطلوب.");
        }
    }
}
