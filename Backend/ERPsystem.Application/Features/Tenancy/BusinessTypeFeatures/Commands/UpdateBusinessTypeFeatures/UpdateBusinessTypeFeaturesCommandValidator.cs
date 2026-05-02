using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.UpdateBusinessTypeFeatures
{
    /// <summary>
    /// مدقق البيانات لأمر تحديث ميزات النشاط التجاري.
    /// </summary>
    public class UpdateBusinessTypeFeaturesCommandValidator : AbstractValidator<UpdateBusinessTypeFeaturesCommand>
    {
        public UpdateBusinessTypeFeaturesCommandValidator()
        {
            RuleFor(v => v.BusinessType)
                .NotEmpty().WithMessage("يجب اختيار نوع النشاط التجاري.");

            RuleFor(v => v.FeatureKeys)
                .NotNull().WithMessage("قائمة الميزات لا يمكن أن تكون فارغة.");
        }
    }
}
