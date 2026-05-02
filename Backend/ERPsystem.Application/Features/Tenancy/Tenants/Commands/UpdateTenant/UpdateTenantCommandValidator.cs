using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.UpdateTenant
{
    /// <summary>
    /// التحقق من مدخلات أمر تعديل المنشأة.
    /// </summary>
    public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم المنشأة مطلوب.")
                .MaximumLength(150).WithMessage("اسم المنشأة يجب ألا يتجاوز 150 حرفاً.");

            RuleFor(x => x.SubscriptionPlanId)
                .NotEmpty().WithMessage("الاشتراك مطلوب.");
        }
    }
}
