using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory
{
    /// <summary>
    /// التحقق من مدخلات استعلام تاريخ الاشتراك.
    /// </summary>
    public class GetTenantSubscriptionHistoryQueryValidator : AbstractValidator<GetTenantSubscriptionHistoryQuery>
    {
        public GetTenantSubscriptionHistoryQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");
        }
    }
}
