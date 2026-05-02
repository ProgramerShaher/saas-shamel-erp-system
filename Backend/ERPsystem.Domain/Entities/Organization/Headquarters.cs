using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// المقر الرئيسي أو الكيان القانوني الأم للمنشأة التجارية.
    /// </summary>
    public class Headquarters : TenantBaseEntity
    {
        /// <summary>الاسم القانوني للمقر (يُطبع في الترويسات الرسمية)</summary>
        public string LegalName { get; set; } = null!;
        
        /// <summary>الرقم الضريبي الموحد للمنشأة (VAT Number)</summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>رقم السجل التجاري العام</summary>
        public string? CommercialRegNumber { get; set; }
        
        /// <summary>العنوان الوطني أو العنوان المادي</summary>
        public string? Address { get; set; }
        
        /// <summary>رقم الهاتف العام للشركة</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>البريد الإلكتروني للشكاوى والدعم</summary>
        public string? Email { get; set; }
        
        /// <summary>رابط شعار المنشأة الرسمي</summary>
        public string? LogoUrl { get; set; }

        /// <summary>شهر بداية السنة المالية (مثال: شهر 1 يعني يناير)</summary>
        public int FiscalYearStartMonth { get; set; } = 1;

        /// <summary>العملة الأساسية لتقييم المنشأة مالياً في الميزانيات</summary>
        public string CurrencyCode { get; set; } = "SAR";

        /// <summary>جميع الفروع التي تنضوي تحت هذا الكيان القانوني</summary>
        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
