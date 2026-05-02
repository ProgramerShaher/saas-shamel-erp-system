using System;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.UpdateTenant
{
    /// <summary>
    /// أمر تعديل بيانات منشأة.
    /// </summary>
    public class UpdateTenantCommand : IRequest<BaseResponse<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public BusinessType BusinessType { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public bool IsActive { get; set; }
        public string? LogoUrl { get; set; }
        public string? PrimaryColor { get; set; }
    }
}
