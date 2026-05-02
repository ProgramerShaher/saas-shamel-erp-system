using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.DeleteTenant
{
    /// <summary>
    /// أمر حذف/تعطيل منشأة.
    /// </summary>
    public class DeleteTenantCommand : IRequest<BaseResponse<Guid>>
    {
        public Guid Id { get; set; }
    }
}
