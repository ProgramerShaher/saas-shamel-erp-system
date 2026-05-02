using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Tenancy
{
    /// <summary>
    /// أعلام الميزات (Feature Toggles) لكل منشأة.
    /// يتحكم في تفاصيل النظام الديناميكية مثل إظهار/إخفاء فواتير الضرائب، تواريخ الصلاحية، المقاسات.
    /// </summary>
    public class TenantFeatureFlag : BaseEntity
    {
        /// <summary>معرف المنشأة التابع لها الميزة</summary>
        public Guid TenantId { get; set; }
        
        /// <summary>المنشأة</summary>
        public virtual Tenant Tenant { get; set; } = null!;

        /// <summary>المفتاح البرمجي للميزة (مثال: SERIAL_NUMBERS أو EXPIRY_DATES)</summary>
        public string FeatureKey { get; set; } = null!;
        
        /// <summary>حالة التفعيل</summary>
        public bool IsEnabled { get; set; } = false;

        /// <summary>إعدادات إضافية بصيغة JSON للميزة</summary>
        public string? ConfigJson { get; set; }

        /// <summary>تاريخ وقت التفعيل</summary>
        public DateTime? EnabledAt { get; set; }
        
        /// <summary>معرف مستخدم الدعم الفني أو المدير الذي قام بالتفعيل</summary>
        public Guid? EnabledByUserId { get; set; }
    }
}
