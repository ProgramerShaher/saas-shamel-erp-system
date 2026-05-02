using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.UpdateTenant
{
    /// <summary>
    /// معالج أمر التعديل على بيانات المنشأة.
    /// </summary>
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateTenantCommandHandler> _logger;

        public UpdateTenantCommandHandler(
            IApplicationDbContext context,
            ILogger<UpdateTenantCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(
            UpdateTenantCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ تعديل منشأة | المعرف: [{TenantId}]", request.Id);

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (tenant == null)
            {
                _logger.LogWarning("⚠ فشل التعديل — المنشأة غير موجودة | المعرف: [{TenantId}]", request.Id);
                return new BaseResponse<Guid>("المنشأة غير موجودة.");
            }

            // تعديل البيانات يدوياً (حسب قاعدة الدستور: لا AutoMapper للـ Commands)
            tenant.Name = request.Name;
            tenant.BusinessType = request.BusinessType;
            tenant.SubscriptionPlanId = request.SubscriptionPlanId;
            tenant.IsActive = request.IsActive;
            tenant.LogoUrl = request.LogoUrl;
            tenant.PrimaryColor = request.PrimaryColor;

            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم تعديل المنشأة بنجاح | المعرف: [{TenantId}]", tenant.Id);
            return new BaseResponse<Guid>(tenant.Id, "تم تعديل المنشأة بنجاح.");
        }
    }
}
