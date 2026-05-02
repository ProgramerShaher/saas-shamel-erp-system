using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPsystem.Domain.Entities.Tenancy;
using ERPsystem.Domain.Enums.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace ERPsystem.Infrastructure.Persistence
{
    /// <summary>
    /// مسؤول عن ضخ البيانات الأولية (Seeding) في قاعدة البيانات.
    /// يضمن وجود الإعدادات الأساسية والقوالب عند تشغيل النظام لأول مرة.
    /// </summary>
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDatabaseAsync(ApplicationDbContext context)
        {
            // 1. Seed ميزات الأنشطة التجارية (Business Type Features)
            if (!await context.BusinessTypeFeatures.AnyAsync())
            {
                var featureTemplates = new List<BusinessTypeFeature>();

                foreach (BusinessType type in Enum.GetValues(typeof(BusinessType)))
                {
                    // --- ميزات مشتركة لكل الأنشطة ---
                    AddFeature(featureTemplates, type, FeatureKey.POS_MODULE);
                    AddFeature(featureTemplates, type, FeatureKey.INVENTORY_ADJUSTMENT);
                    AddFeature(featureTemplates, type, FeatureKey.MULTI_UNIT);

                    // --- ميزات مخصصة حسب النوع ---
                    switch (type)
                    {
                        case BusinessType.Pharmacy:
                            AddFeature(featureTemplates, type, FeatureKey.PHARMACY_MODULE);
                            AddFeature(featureTemplates, type, FeatureKey.EXPIRY_DATES);
                            AddFeature(featureTemplates, type, FeatureKey.BATCH_NUMBERS);
                            AddFeature(featureTemplates, type, FeatureKey.MULTI_BARCODE);
                            break;

                        case BusinessType.Supermarket:
                        case BusinessType.Hypermarket:
                        case BusinessType.GroceryStore:
                            AddFeature(featureTemplates, type, FeatureKey.EXPIRY_DATES);
                            AddFeature(featureTemplates, type, FeatureKey.MULTI_BARCODE);
                            AddFeature(featureTemplates, type, FeatureKey.ADVANCED_DISCOUNTS);
                            AddFeature(featureTemplates, type, FeatureKey.LOYALTY_POINTS);
                            if (type != BusinessType.GroceryStore) 
                                AddFeature(featureTemplates, type, FeatureKey.MULTI_BRANCH);
                            break;

                        case BusinessType.ClothingStore:
                        case BusinessType.ShoesStore:
                            AddFeature(featureTemplates, type, FeatureKey.PRODUCT_VARIANTS);
                            AddFeature(featureTemplates, type, FeatureKey.MULTI_BARCODE);
                            AddFeature(featureTemplates, type, FeatureKey.ADVANCED_DISCOUNTS);
                            break;

                        case BusinessType.ElectronicsStore:
                        case BusinessType.HardwareStore:
                            AddFeature(featureTemplates, type, FeatureKey.SERIAL_NUMBERS);
                            AddFeature(featureTemplates, type, FeatureKey.WHOLESALE_MODULE);
                            break;

                        case BusinessType.Mall:
                            AddFeature(featureTemplates, type, FeatureKey.MULTI_BRANCH);
                            AddFeature(featureTemplates, type, FeatureKey.HEADQUARTERS_STRUCTURE);
                            AddFeature(featureTemplates, type, FeatureKey.COST_CENTERS);
                            AddFeature(featureTemplates, type, FeatureKey.ADVANCED_REPORTS);
                            AddFeature(featureTemplates, type, FeatureKey.EXECUTIVE_DASHBOARD);
                            break;
                    }
                }

                await context.BusinessTypeFeatures.AddRangeAsync(featureTemplates);
                await context.SaveChangesAsync();
            }
        }

        private static void AddFeature(List<BusinessTypeFeature> list, BusinessType type, FeatureKey feature)
        {
            list.Add(new BusinessTypeFeature 
            { 
                BusinessType = type, 
                FeatureKey = feature, 
                IsEnabledByDefault = true 
            });
        }
    }
}
