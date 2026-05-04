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
        private readonly IFileService _fileService;

        public UpdateTenantCommandHandler(
            IApplicationDbContext context,
            ILogger<UpdateTenantCommandHandler> logger,
            IFileService fileService)
        {
            _context = context;
            _logger = logger;
            _fileService = fileService;
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
            // معالجة تغيير الصورة (حذف القديمة وحفظ الجديدة إذا كانت Base64)
            if (!string.IsNullOrWhiteSpace(request.LogoUrl) && request.LogoUrl.StartsWith("data:image"))
            {
                if (!string.IsNullOrWhiteSpace(tenant.LogoUrl))
                {
                    _fileService.DeleteFile(tenant.LogoUrl);
                }
                var newLogoUrl = await _fileService.SaveBase64ImageAsync(request.LogoUrl, "tenants", tenant.Slug, cancellationToken);
                tenant.LogoUrl = newLogoUrl ?? request.LogoUrl;
            }
            else if (string.IsNullOrWhiteSpace(request.LogoUrl) && !string.IsNullOrWhiteSpace(tenant.LogoUrl))
            {
                _fileService.DeleteFile(tenant.LogoUrl);
                tenant.LogoUrl = null;
            }
            else if (!string.IsNullOrWhiteSpace(request.LogoUrl))
            {
                 // في حال تم إرسال مسار URL عادي للصورة (لم تتغير)
                 tenant.LogoUrl = request.LogoUrl;
            }
            tenant.PrimaryColor = request.PrimaryColor;

            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم تعديل المنشأة بنجاح | المعرف: [{TenantId}]", tenant.Id);
            return new BaseResponse<Guid>(tenant.Id, "تم تعديل المنشأة بنجاح.");
        }
    }
}
