using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace ERPsystem.Application.Common.Behaviors
{
    /// <summary>
    /// سلوك الخط الأنبوبي للتحقق من صحة البيانات (Validation).
    /// يجمع كل الـ Validators المسجلة للـ Request الحالي تلقائياً ابن ويشغلها قبل الـ Handler.
    /// في حالة وجود أخطاء يوقف التنفيذ فوراً ويعيد قائمة الأخطاء — ولا يصل الطلب للـ Handler.
    /// يُطبق قاعدة ثانياً من الدستور: التحقق من البيانات (Validation) عبر FluentValidation.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// يشغل جميع الـ Validators المرتبطة بالـ Request.
        /// إذا كان هناك خطأ، يرمي استثناء ValidationException يحتوي على تفاصيل الأخطاء.
        /// </summary>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // تشغيل جميع الـ Validators بشكل غير متزامن في نفس الوقت
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // جمع كل الأخطاء من كل الـ Validators
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count > 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
