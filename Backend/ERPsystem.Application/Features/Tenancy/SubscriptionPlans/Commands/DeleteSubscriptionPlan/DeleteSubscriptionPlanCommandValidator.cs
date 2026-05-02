using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.DeleteSubscriptionPlan
{
    /// <summary>
    /// مدقق مدخلات حذف الباقة.
    /// </summary>
    public class DeleteSubscriptionPlanCommandValidator : AbstractValidator<DeleteSubscriptionPlanCommand>
    {
        public DeleteSubscriptionPlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("الرقم التعريفي للباقة مطلوب.");
        }
    }
}
