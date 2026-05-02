using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Configuration;

namespace ERPsystem.Domain.Entities.Configuration
{
    /// <summary>
    /// قوالب الطباعة الديناميكية للفواتير أو الباركودات (Receipt Print Templates).
    /// </summary>
    public class PrintTemplate : TenantBaseEntity
    {
        /// <summary>اسم القالب المرئي</summary>
        public string TemplateName { get; set; } = null!;
        
        /// <summary>نوع الاستهداف (فاتورة حرارية؟ A4؟ لصاقة باركود؟)</summary>
        public string TargetDocument { get; set; } = null!;
        
        /// <summary>مقاس طابعة الورق المتوافقة</summary>
        public PrintPageSize PageSize { get; set; }

        /// <summary>تصميم الهيكل والمحتوى كـ HTML / رسائل الزات تيبل لتأدية الطباعة</summary>
        public string TemplateDesignJsonHtml { get; set; } = null!;

        /// <summary>هل هذا القالب هو التصميم الافتراضي المعتمد للوثيقة المحددة بالمنشأة</summary>
        public bool IsDefault { get; set; } = false;
    }
}
