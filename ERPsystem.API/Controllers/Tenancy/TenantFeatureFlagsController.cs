using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Commands.UpsertTenantFeatureFlag;
using ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPsystem.API.Controllers.Tenancy
{
    /// <summary>
    /// نقطة نهاية لإدارة مميزات المنشآت (Feature Flags).
    /// </summary>
    public class TenantFeatureFlagsController : BaseApiController
    {
        public TenantFeatureFlagsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{tenantId:guid}")]
        [ProducesResponseType(typeof(BaseResponse<List<TenantFeatureFlagDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllForTenant([FromRoute] Guid tenantId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new GetTenantFeatureFlagsQuery { TenantId = tenantId }, cancellationToken);
                return OkResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }

        [HttpPost("upsert")]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upsert([FromBody] UpsertTenantFeatureFlagCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(command, cancellationToken);
                return result.Succeeded ? OkResponse(result) : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }
    }
}
