using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.DeleteTenant
{
    /// <summary>
    /// التحقق من مدخلات حذف المنشأة.
    /// </summary>
    public class DeleteTenantCommandValidator : AbstractValidator<DeleteTenantCommand>
    {
        public DeleteTenantCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("الرقم التعريفي للمنشأة مطلوب.");
        }
    }
}
