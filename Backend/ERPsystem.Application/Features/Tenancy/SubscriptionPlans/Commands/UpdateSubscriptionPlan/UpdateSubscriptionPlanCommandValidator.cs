using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.UpdateSubscriptionPlan
{
    /// <summary>
    /// مدقق أمر تعديل बाقة الاشتراك.
    /// </summary>
    public class UpdateSubscriptionPlanCommandValidator : AbstractValidator<UpdateSubscriptionPlanCommand>
    {
        public UpdateSubscriptionPlanCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("الرقم التعريفي للباقة مطلوب.");

            RuleFor(x => x.PlanName)
                .NotEmpty().WithMessage("اسم الباقة مطلوب.")
                .MaximumLength(150).WithMessage("اسم الباقة يجب ألا يتجاوز 150 حرفاً.");

            RuleFor(x => x.MonthlyPrice)
                .GreaterThanOrEqualTo(0).WithMessage("السعر الشهري الباقة يجب أن يكون صفراً أو أكثر.");
        }
    }
}
