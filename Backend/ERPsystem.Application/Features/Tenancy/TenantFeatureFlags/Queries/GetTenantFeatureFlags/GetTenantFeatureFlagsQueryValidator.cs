using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags
{
    /// <summary>
    /// التحقق من مدخلات استعلام المميزات.
    /// </summary>
    public class GetTenantFeatureFlagsQueryValidator : AbstractValidator<GetTenantFeatureFlagsQuery>
    {
        public GetTenantFeatureFlagsQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");
        }
    }
}
