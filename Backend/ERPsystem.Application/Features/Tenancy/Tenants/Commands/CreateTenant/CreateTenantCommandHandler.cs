using System;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Commands.CreateTenant
{
    /// <summary>
    /// المعالج المسؤول عن تنفيذ منطق إنشاء منشأة جديدة (Handler).
    /// يتحدث مباشرة مع قاعدة البيانات عبر IApplicationDbContext (بدون Repository).
    /// يطبق قاعدة سادساً من الدستور للـ Logging: يسجل بدء العملية، نجاحها، وفشلها. 
    /// </summary>
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateTenantCommandHandler> _logger;
        private readonly ITenantSetupService _tenantSetupService;
        private readonly IFileService _fileService;

        public CreateTenantCommandHandler(
            IApplicationDbContext context,
            ILogger<CreateTenantCommandHandler> logger,
            ITenantSetupService tenantSetupService,
            IFileService fileService)
        {
            _context = context;
            _logger = logger;
            _tenantSetupService = tenantSetupService;
            _fileService = fileService;
        }

        /// <summary>
        /// ينفذ عملية إنشاء المنشأة الجديدة في قاعدة البيانات.
        /// </summary>
        /// <param name="request">بيانات المنشأة المراد إنشاؤها</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <returns>BaseResponse يحتوي على معرف المنشأة الجديدة</returns>
        public async Task<BaseResponse<Guid>> Handle(
            CreateTenantCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "▶ بدأ إنشاء منشأة جديدة | الاسم: [{TenantName}] | النطاق: [{Slug}]",
                request.Name, request.Slug);

            try
            {
                // التحقق من عدم تكرار الـ Slug (قاعدة عمل)
                var slugExists = await _context.Tenants
                    .AnyAsync(t => t.Slug == request.Slug.ToLower(), cancellationToken);

                if (slugExists)
                {
                    _logger.LogWarning(
                        "⚠ فشل إنشاء المنشأة — النطاق [{Slug}] مسجل مسبقاً.",
                        request.Slug);

                    return new BaseResponse<Guid>(
                        $"النطاق الفرعي '{request.Slug}' مسجل مسبقاً، يرجى اختيار نطاق آخر.");
                }

                // معالجة الصورة في حال تم إرسالها كـ Base64
                var logoUrl = await _fileService.SaveBase64ImageAsync(request.LogoUrl, "tenants", request.Slug, cancellationToken);

                // بناء كيان المنشأة يدوياً (لا AutoMapper من Command إلى Entity — قاعدة ثانياً)
                var tenant = new Tenant
                {
                    Name          = request.Name,
                    Slug          = request.Slug.ToLower(),
                    DatabaseSchema = request.Slug.ToLower().Replace("-", "_"), 
                    BusinessType  = request.BusinessType,
                    SubscriptionPlanId = request.SubscriptionPlanId,
                    PrimaryColor  = request.PrimaryColor,
                    LogoUrl       = logoUrl ?? request.LogoUrl,
                    IsActive      = true
                };

                // توليد وإضافة الميزات الافتراضية بناءً على نوع النشاط (القاعدة السابعة)
                tenant.FeatureFlags = _tenantSetupService.GenerateDefaultFeatureFlags(request.BusinessType);

                await _context.Tenants.AddAsync(tenant, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "✔ تم إنشاء المنشأة بنجاح | المعرف: [{TenantId}] | الاسم: [{TenantName}]",
                    tenant.Id, tenant.Name);

                return new BaseResponse<Guid>(tenant.Id, "تم إنشاء المنشأة بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "✘ خطأ غير متوقع أثناء إنشاء المنشأة [{TenantName}]",
                    request.Name);
                throw;
            }
        }
    }
}
