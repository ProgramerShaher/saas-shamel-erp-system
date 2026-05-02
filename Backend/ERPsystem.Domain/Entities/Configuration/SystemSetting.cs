using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Configuration;

namespace ERPsystem.Domain.Entities.Configuration
{
    /// <summary>
    /// مفاتيح إعدادات النظام الحرة وحفظ المتغيرات للمنشأة (Settings Key-Value Maps).
    /// </summary>
    public class SystemSetting : TenantBaseEntity
    {
        /// <summary>معرف أو مجموعة الإعداد (مثال: Tax_Settings، Theme_Settings)</summary>
        public string SettingGroup { get; set; } = null!;
        
        /// <summary>المفتاح السري لطلب الإعداد (مثال: Default_Language)</summary>
        public string SettingKey { get; set; } = null!;
        
        /// <summary>القيمة المخزنة نصيا</summary>
        public string SettingValue { get; set; } = null!;
        
        /// <summary>نوع البيانات لتحويلها واجتذابها بالمقاس (من النص للبولين)</summary>
        public SettingDataType DataType { get; set; } = SettingDataType.String;

        /// <summary>فرع محدد يملك إعداد غير البقية إن طلب</summary>
        public Guid? BranchId { get; set; }
    }
}
