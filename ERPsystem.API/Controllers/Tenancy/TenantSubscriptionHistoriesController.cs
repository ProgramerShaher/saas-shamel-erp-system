using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Commands.AddTenantSubscriptionHistory;
using ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPsystem.API.Controllers.Tenancy
{
    /// <summary>
    /// نقطة نهاية لإدارة تاريخ اشتراكات المنشأة.
    /// </summary>
    public class TenantSubscriptionHistoriesController : BaseApiController
    {
        public TenantSubscriptionHistoriesController(IMediator mediator) : base(mediator) { }

        [HttpGet("{tenantId:guid}")]
        [ProducesResponseType(typeof(BaseResponse<List<TenantSubscriptionHistoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistoryForTenant([FromRoute] Guid tenantId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new GetTenantSubscriptionHistoryQuery { TenantId = tenantId }, cancellationToken);
                return OkResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddTenantSubscriptionHistoryCommand command, CancellationToken cancellationToken = default)
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
    }
}
