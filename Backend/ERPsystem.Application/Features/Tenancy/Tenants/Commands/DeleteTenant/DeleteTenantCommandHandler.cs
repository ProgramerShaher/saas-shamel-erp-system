using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.DeleteTenant
{
    /// <summary>
    /// معالج أمر حذف المنشأة (يتيح خيار الحذف أو الإيقاف كمثال).
    /// </summary>
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DeleteTenantCommandHandler> _logger;

        public DeleteTenantCommandHandler(
            IApplicationDbContext context,
            ILogger<DeleteTenantCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(
            DeleteTenantCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ حذف المنشأة | المعرف: [{TenantId}]", request.Id);

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (tenant == null)
            {
                _logger.LogWarning("⚠ فشل الحذف — المنشأة غير موجودة | المعرف: [{TenantId}]", request.Id);
                return new BaseResponse<Guid>("المنشأة غير موجودة.");
            }

            // إما حذف كامل أو إيقاف (Soft Delete / Deactivate)
            // نعتمد هنا الحذف المباشر طالما لا يعترضنا قيد Foreign Key، إذا كان يوجد سنستخدم Soft Delete.
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم حذف المنشأة بنجاح | المعرف: [{TenantId}]", tenant.Id);
            return new BaseResponse<Guid>(tenant.Id, "تم حذف المنشأة بنجاح.");
        }
    }
}
