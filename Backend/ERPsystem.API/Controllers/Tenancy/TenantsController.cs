using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.Tenants.Commands.CreateTenant;
using ERPsystem.Application.Features.Tenancy.Tenants.Commands.UpdateTenant;
using ERPsystem.Application.Features.Tenancy.Tenants.Commands.DeleteTenant;
using ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants;
using ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantDto = ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById.TenantDto;

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
        [ProducesResponseType(typeof(PagedResponse<TenantDto>), StatusCodes.Status200OK)]
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

        // ══════════════════════════════════════════════════════════════
        //  GET api/v1/tenants/{id}
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// استرجاع بيانات منشأة واحدة بواسطة المعرف.
        /// </summary>
        /// <param name="id">معرف المنشأة</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <response code="200">تم استرجاع المنشأة بنجاح</response>
        /// <response code="404">المنشأة غير موجودة</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<TenantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetTenantByIdQuery { Id = id }, cancellationToken);

            return OkResponse(result);
        }

        // ══════════════════════════════════════════════════════════════
        //  PUT api/v1/tenants/{id}
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// تعديل بيانات منشأة موجودة.
        /// </summary>
        /// <param name="id">معرف المنشأة المراد تعديلها</param>
        /// <param name="command">البيانات الجديدة للمنشأة</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <response code="200">تم تعديل المنشأة بنجاح</response>
        /// <response code="400">بيانات مُدخلة غير صحيحة (Validation Errors)</response>
        /// <response code="404">المنشأة غير موجودة</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateTenantCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest(new BaseResponse<Guid>("المعرف في مسار الرابط لا يتطابق مع المعرف في جسم الطلب."));
                }

                var result = await Mediator.Send(command, cancellationToken);

                return result.Succeeded
                    ? OkResponse(result)
                    : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  DELETE api/v1/tenants/{id}
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// حذف/إيقاف منشأة بواسطة المعرف.
        /// </summary>
        /// <param name="id">معرف المنشأة</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <response code="200">تم حذف المنشأة بنجاح</response>
        /// <response code="404">المنشأة غير موجودة</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new DeleteTenantCommand { Id = id }, cancellationToken);

                return result.Succeeded
                    ? OkResponse(result)
                    : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }
    }
}
