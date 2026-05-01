using FluentValidation;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.CreateTenant
{
    /// <summary>
    /// قواعد التحقق من صحة بيانات إنشاء المنشأة.
    /// يُسجَّل ويُنفَّذ تلقائياً عبر الـ ValidationBehavior في خط الأنابيب (Pipeline).
    /// يضمن عدم وصول بيانات ناقصة أو خاطئة للـ Handler أو قاعدة البيانات.
    /// </summary>
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            // التحقق من اسم المنشأة
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("اسم المنشأة لا يمكن أن يكون فارغاً.")
                .MinimumLength(3)
                    .WithMessage("اسم المنشأة يجب أن يكون 3 أحرف على الأقل.")
                .MaximumLength(200)
                    .WithMessage("اسم المنشأة لا يمكن أن يتجاوز 200 حرف.");

            // التحقق من الـ Slug
            RuleFor(x => x.Slug)
                .NotEmpty()
                    .WithMessage("النطاق الفرعي (Slug) مطلوب.")
                .MinimumLength(3)
                    .WithMessage("الـ Slug يجب أن يكون 3 أحرف على الأقل.")
                .MaximumLength(100)
                    .WithMessage("الـ Slug لا يمكن أن يتجاوز 100 حرف.")
                .Matches(@"^[a-z0-9\-]+$")
                    .WithMessage("الـ Slug يجب أن يحتوي على أحرف صغيرة وأرقام وشرطات فقط.");

            // التحقق من نوع النشاط التجاري
            RuleFor(x => x.BusinessType)
                .IsInEnum()
                    .WithMessage("نوع النشاط التجاري غير صحيح.");

            // التحقق من باقة الاشتراك
            RuleFor(x => x.SubscriptionPlanId)
                .NotEmpty()
                    .WithMessage("يجب اختيار باقة اشتراك.");

            // التحقق الاختياري من اللون إن أُرسل
            When(x => x.PrimaryColor != null, () =>
            {
                RuleFor(x => x.PrimaryColor)
                    .Matches(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                        .WithMessage("اللون يجب أن يكون بصيغة Hex صحيحة (مثال: #FF5733).");
            });
        }
    }
}
