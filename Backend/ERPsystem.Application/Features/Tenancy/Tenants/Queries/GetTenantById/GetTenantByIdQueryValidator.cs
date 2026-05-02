using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById
{
    /// <summary>
    /// ملف التحقق من صحة استعلام جلب منشأة بواسطة المعرف.
    /// </summary>
    public class GetTenantByIdQueryValidator : AbstractValidator<GetTenantByIdQuery>
    {
        public GetTenantByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف المنشأة مطلوب.");
        }
    }
}
