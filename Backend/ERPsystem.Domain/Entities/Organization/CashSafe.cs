using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Organization;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// الخزينة النقدية للفرع أو درج الكاشير. تمثل حركات الإيداع والسحب الفوري النقدي.
    /// </summary>
    public class CashSafe : TenantBaseEntity
    {
        /// <summary>الفرع المتواجد به الخزنة</summary>
        public Guid BranchId { get; set; }
        
        /// <summary>الفرع</summary>
        public virtual Branch Branch { get; set; } = null!;

        /// <summary>اسم الخزنة التفصيلي (الخزنة المركزية، صندوق المرتجعات)</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>نوع الخزنة (صندوق رئيسي، فرعي، درج كاشير)</summary>
        public CashSafeType SafeType { get; set; }

        /// <summary>معرف الحساب المحاسبي (GlAccountId) لتسجيل وتوليد القيود المزدوجة مباشرة إليه</summary>
        public Guid GlAccountId { get; set; }

        /// <summary>رصيد النقدية الحالي للخزنة بعد الجمع والطرح التلقائي للفواتير والسندات</summary>
        public decimal CurrentBalance { get; set; } = 0;

        /// <summary>عملة هذا الصندوق (الصندوق قد يكون بعملة اليورو أو الريال للتقييم)</summary>
        public string CurrencyCode { get; set; } = "SAR";
        
        /// <summary>حالة العمل للدرج لتعزيز الإغلاق</summary>
        public bool IsActive { get; set; } = true;
    }
}
