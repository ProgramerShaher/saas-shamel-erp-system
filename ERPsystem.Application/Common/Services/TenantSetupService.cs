using System;
using System.Collections.Generic;
using System.Linq;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Domain.Entities.Tenancy;
using ERPsystem.Domain.Enums.Tenancy;

namespace ERPsystem.Application.Common.Services
{
    /// <summary>
    /// خدمة تنفيذية لإعداد المنشآت الجديدة وتحديد الميزات الافتراضية
    /// (Feature Flags) التي تناسب طبيعة النشاط التجاري.
    /// النسخة المحدثة: تقرأ من قاعدة البيانات لتوفير أقصى قدر من المرونة.
    /// </summary>
    public class TenantSetupService : ITenantSetupService
    {
        private readonly IApplicationDbContext _context;

        public TenantSetupService(IApplicationDbContext context)
        {
            _context = context;
        }

        public List<TenantFeatureFlag> GenerateDefaultFeatureFlags(BusinessType businessType)
        {
            // جلب الميزات المخصصة لهذا النشاط من جدول القوالب
            // إذا لم توجد ميزات مخصصة في الجدول، يمكننا وضع ميزات أساسية (Fallback)
            var templateFeatures = _context.BusinessTypeFeatures
                .Where(x => x.BusinessType == businessType && x.IsEnabledByDefault)
                .ToList();

            if (!templateFeatures.Any())
            {
                // ميزات أساسية (Fallback) في حال كان الجدول فارغاً
                return new List<TenantFeatureFlag>
                {
                    new TenantFeatureFlag { FeatureKey = FeatureKey.POS_MODULE.ToString(), IsEnabled = true, EnabledAt = DateTime.UtcNow },
                    new TenantFeatureFlag { FeatureKey = FeatureKey.INVENTORY_ADJUSTMENT.ToString(), IsEnabled = true, EnabledAt = DateTime.UtcNow }
                };
            }

            return templateFeatures
                .Select(f => new TenantFeatureFlag
                {
                    FeatureKey = f.FeatureKey.ToString(),
                    IsEnabled = true,
                    EnabledAt = DateTime.UtcNow
                })
                .ToList();
        }
    }
}
