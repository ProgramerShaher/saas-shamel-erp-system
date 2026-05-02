using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Commands.AddTenantSubscriptionHistory
{
    /// <summary>
    /// التحقق من مدخلات إضافة السجل التاريخي للاشتراك.
    /// </summary>
    public class AddTenantSubscriptionHistoryCommandValidator : AbstractValidator<AddTenantSubscriptionHistoryCommand>
    {
        public AddTenantSubscriptionHistoryCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");
            RuleFor(x => x.PlanId).NotEmpty().WithMessage("معرف الباقة مطلوب.");
            RuleFor(x => x.EffectiveDate).NotEmpty().WithMessage("تاريخ السريان مطلوب.");
        }
    }
}
