using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.Tenants.Commands.CreateTenant;
using ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPsystem.API.Controllers.Tenancy
{
    /// <summary>
    /// نقطة نهاية API لإدارة المنشآت (Tenants).
    /// Controller خالٍ تماماً من منطق الأعمال — يُمرر فقط للـ MediatR.
    /// يُطبق قاعدة رابعاً من الدستور: Controllers بلا Business Logic.
    /// </summary>
    public class TenantsController : BaseApiController
    {
        public TenantsController(IMediator mediator) : base(mediator) { }

        // ══════════════════════════════════════════════════════════════
        //  GET api/v1/tenants?pageNumber=1&pageSize=10&searchKeyword=
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// استرجاع قائمة المنشآت مع دعم الـ Pagination والبحث.
        /// </summary>
        /// <param name="pageNumber">رقم الصفحة (الافتراضي: 1)</param>
        /// <param name="pageSize">حجم الصفحة (الافتراضي: 10، الحد الأقصى: 500)</param>
        /// <param name="searchKeyword">كلمة البحث في الاسم أو النطاق (اختياري)</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <response code="200">تم استرجاع القائمة بنجاح مع معلومات الصفحات</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<TenantVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize   = 10,
            [FromQuery] string? searchKeyword = null,
            CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetTenantsQuery
            {
                PageNumber    = pageNumber,
                PageSize      = pageSize,
                SearchKeyword = searchKeyword
            }, cancellationToken);

            return OkResponse(result);
        }

        // ══════════════════════════════════════════════════════════════
        //  POST api/v1/tenants
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// إنشاء منشأة جديدة في النظام.
        /// </summary>
        /// <param name="command">بيانات المنشأة الجديدة</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <response code="201">تم إنشاء المنشأة بنجاح ويعيد معرف المنشأة الجديدة</response>
        /// <response code="400">بيانات مُدخلة غير صحيحة (Validation Errors)</response>
        /// <response code="422">تعارض في قواعد العمل (Slug مكرر مثلاً)</response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreateTenantCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(command, cancellationToken);

                return result.Succeeded
                    ? CreatedResponse(result)
                    : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }
    }
}
