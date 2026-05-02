using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.CreateSubscriptionPlan;
using ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.UpdateSubscriptionPlan;
using ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.DeleteSubscriptionPlan;
using ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans;
using ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionPlanDto = ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans.SubscriptionPlanDto;

namespace ERPsystem.API.Controllers.Tenancy
{
    /// <summary>
    /// نقطة نهاية لإدارة باقات الاشتراك المتاحة في النظام.
    /// </summary>
    public class SubscriptionPlansController : BaseApiController
    {
        public SubscriptionPlansController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<SubscriptionPlanDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchKeyword = null,
            CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetSubscriptionPlansQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchKeyword = searchKeyword
            }, cancellationToken);

            return OkResponse(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById.SubscriptionPlanDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetSubscriptionPlanByIdQuery { Id = id }, cancellationToken);
            return OkResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(command, cancellationToken);
                return result.Succeeded ? CreatedResponse(result) : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSubscriptionPlanCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id != command.Id) return BadRequest(new BaseResponse<Guid>("المعرف في مسار الرابط لا يتطابق مع المعرف في الطلب."));

                var result = await Mediator.Send(command, cancellationToken);
                return result.Succeeded ? OkResponse(result) : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new DeleteSubscriptionPlanCommand { Id = id }, cancellationToken);
                return result.Succeeded ? OkResponse(result) : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }
    }
}
