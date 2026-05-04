using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERPsystem.Application.Common.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ERPsystem.API.Controllers
{
    /// <summary>
    /// الكنترولر الجذر الذي ترث منه جميع كنترولرات الـ API.
    /// يوفر دوال مساعدة مشتركة لإرجاع الردود الموحدة (BaseResponse)
    /// بدلاً من تكرار كود الـ StatusCode في كل كنترولر.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// يُرجع 200 OK مع الـ BaseResponse مباشرة.
        /// </summary>
        protected IActionResult OkResponse<T>(BaseResponse<T> response)
            => Ok(response);

        /// <summary>
        /// يُرجع 201 Created مع الـ BaseResponse عند نجاح الإنشاء.
        /// </summary>
        protected IActionResult CreatedResponse<T>(BaseResponse<T> response)
            => StatusCode(StatusCodes.Status201Created, response);

        /// <summary>
        /// يُرجع 400 BadRequest مع تفاصيل أخطاء الـ Validation.
        /// </summary>
        protected IActionResult ValidationErrorResponse(ValidationException ex)
        {
            var response = new BaseResponse<object>("فشل التحقق من البيانات المُدخلة.")
            {
                Errors = ex.Errors.Select(e => e.ErrorMessage).ToList()
            };
            return BadRequest(response);
        }

        /// <summary>
        /// يُرجع 422 Unprocessable Entity عند فشل قاعدة عمل (Business Rule Failure).
        /// </summary>
        protected IActionResult BusinessRuleFailureResponse<T>(BaseResponse<T> response)
            => UnprocessableEntity(response);

        /// <summary>
        /// يُرجع 404 Not Found مع رسالة واضحة.
        /// </summary>
        protected IActionResult NotFoundResponse(string message)
            => NotFound(new BaseResponse<object>(message));
    }
}
