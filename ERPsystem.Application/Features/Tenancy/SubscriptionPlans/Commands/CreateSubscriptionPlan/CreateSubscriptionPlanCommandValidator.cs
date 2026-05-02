using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    /// <summary>
    /// التحقق من صحة مدخلات أمر إنشاء باقة.
    /// </summary>
    public class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
    {
        public CreateSubscriptionPlanCommandValidator()
        {
            RuleFor(x => x.PlanName)
                .NotEmpty().WithMessage("اسم الباقة مطلوب.")
                .MaximumLength(150).WithMessage("اسم الباقة يجب ألا يتجاوز 150 حرفاً.");

            RuleFor(x => x.MonthlyPrice)
                .GreaterThanOrEqualTo(0).WithMessage("سعر الباقة يجب أن يكون صفراً أو أكثر.");

            RuleFor(x => x.MaxBranches)
                .GreaterThanOrEqualTo(1).WithMessage("عدد الفروع المسموح به يجب أن يكون 1 على الأقل.");

            RuleFor(x => x.MaxUsers)
                .GreaterThanOrEqualTo(1).WithMessage("عدد المستخدمين يجب أن يكون 1 على الأقل.");
        }
    }
}
