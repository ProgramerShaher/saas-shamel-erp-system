using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById
{
    /// <summary>
    /// استعلام جلب منشأة واحدة بواسطة المعرف.
    /// </summary>
    public class GetTenantByIdQuery : IRequest<BaseResponse<TenantDto>>
    {
        public Guid Id { get; set; }
    }
}
