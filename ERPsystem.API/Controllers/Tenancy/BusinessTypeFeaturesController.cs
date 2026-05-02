using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.AddBusinessTypeFeature;
using ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.RemoveBusinessTypeFeature;
using ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.UpdateBusinessTypeFeatures;
using ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetAllBusinessTypeFeatures;
using ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetBusinessTypeFeatures;
using ERPsystem.Domain.Enums.Tenancy;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPsystem.API.Controllers.Tenancy
{
    /// <summary>
    /// المسؤول عن إدارة قوالب الميزات الخاصة بكل نوع نشاط تجاري.
    /// يسمح لمدير النظام بالتحكم الكامل في الميزات الافتراضية لكل نشاط.
    /// </summary>
    public class BusinessTypeFeaturesController : BaseApiController
    {
        public BusinessTypeFeaturesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// جلب جميع قوالب الميزات لجميع أنواع الأنشطة التجارية في النظام.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<List<AllBusinessTypeFeaturesDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetAllBusinessTypeFeaturesQuery(), cancellationToken);
            return OkResponse(result);
        }

        /// <summary>
        /// جلب ميزات نشاط تجاري محدد.
        /// </summary>
        [HttpGet("{businessType}")]
        [ProducesResponseType(typeof(BaseResponse<List<BusinessTypeFeatureDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] BusinessType businessType, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetBusinessTypeFeaturesQuery { BusinessType = businessType }, cancellationToken);
            return OkResponse(result);
        }

        /// <summary>
        /// إضافة ميزة واحدة جديدة لنشاط تجاري معين.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddBusinessTypeFeatureCommand command, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// تحديث (استبدال) قائمة الميزات بالكامل لنشاط تجاري معين.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateBusinessTypeFeaturesCommand command, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// حذف ميزة واحدة محددة من نشاط تجاري معين.
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove([FromQuery] BusinessType businessType, [FromQuery] FeatureKey featureKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await Mediator.Send(new RemoveBusinessTypeFeatureCommand 
                { 
                    BusinessType = businessType, 
                    FeatureKey = featureKey 
                }, cancellationToken);

                return result.Succeeded ? OkResponse(result) : BusinessRuleFailureResponse(result);
            }
            catch (ValidationException ex)
            {
                return ValidationErrorResponse(ex);
            }
        }
    }
}
