using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    /// <summary>
    /// التحقق من مدخلات استعلام باقة.
    /// </summary>
    public class GetSubscriptionPlanByIdQueryValidator : AbstractValidator<GetSubscriptionPlanByIdQuery>
    {
        public GetSubscriptionPlanByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("الرقم التعريفي للباقة مطلوب.");
        }
    }
}
