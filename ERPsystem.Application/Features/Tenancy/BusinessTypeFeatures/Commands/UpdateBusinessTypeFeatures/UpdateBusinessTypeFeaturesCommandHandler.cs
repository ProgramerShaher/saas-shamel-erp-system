using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.UpdateBusinessTypeFeatures
{
    /// <summary>
    /// معالج أمر تحديث ميزات النشاط التجاري.
    /// يقوم بحذف القديم وإضافة الجديد في معاملة واحدة لضمان التكامل.
    /// </summary>
    public class UpdateBusinessTypeFeaturesCommandHandler : IRequestHandler<UpdateBusinessTypeFeaturesCommand, BaseResponse<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateBusinessTypeFeaturesCommandHandler> _logger;

        public UpdateBusinessTypeFeaturesCommandHandler(IApplicationDbContext context, ILogger<UpdateBusinessTypeFeaturesCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateBusinessTypeFeaturesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ تحديث ميزات النشاط التجاري: [{BusinessType}]", request.BusinessType);

            try
            {
                // 1. جلب الميزات الحالية لهذا النشاط
                var existingFeatures = await _context.BusinessTypeFeatures
                    .Where(x => x.BusinessType == request.BusinessType)
                    .ToListAsync(cancellationToken);

                // 2. حذف الميزات الحالية (استبدال كامل)
                if (existingFeatures.Any())
                {
                    _context.BusinessTypeFeatures.RemoveRange(existingFeatures);
                }

                // 3. إضافة الميزات الجديدة
                var newFeatures = request.FeatureKeys.Select(key => new BusinessTypeFeature
                {
                    BusinessType = request.BusinessType,
                    FeatureKey = key,
                    IsEnabledByDefault = true
                }).ToList();

                if (newFeatures.Any())
                {
                    await _context.BusinessTypeFeatures.AddRangeAsync(newFeatures, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("✔ تم تحديث ميزات النشاط [{BusinessType}] بنجاح. العدد الجديد: [{Count}]", 
                    request.BusinessType, newFeatures.Count);

                return new BaseResponse<bool>(true, "تم تحديث ميزات النشاط بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "✘ فشل تحديث ميزات النشاط التجاري: [{BusinessType}]", request.BusinessType);
                throw;
            }
        }
    }
}
