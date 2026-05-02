using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Purchasing
{
    /// <summary>
    /// المورد / التاجر الذي يتم شراء البضائع منه.
    /// </summary>
    public class Supplier : TenantBaseEntity
    {
        /// <summary>الاسم التجاري للمورد</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>اسم المندوب للتواصل</summary>
        public string? ContactPerson { get; set; }
        
        /// <summary>هاتف المورد</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>الرقم الضريبي للمورد (يستخدم لاحقاً في الإقرار الضريبي)</summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>الايميل للتواصل</summary>
        public string? Email { get; set; }
        
        /// <summary>عنوان شركة المورد</summary>
        public string? Address { get; set; }

        /// <summary>حساب المورد في الدليل المحاسبي (دائنون أمميون)</summary>
        public Guid GlAccountId { get; set; }

        /// <summary>فترة السماح التأمينية بالدفع بالأيام</summary>
        public int CreditPeriodDays { get; set; } = 0;
        
        /// <summary>الحد الائتماني المسموح بالسحب منه كدين</summary>
        public decimal CreditLimit { get; set; } = 0;
        
        /// <summary>حالة التعامل الحالية</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>إجمالي الرصيد القائم علينا لهذا المورد حاليا</summary>
        public decimal CurrentBalance { get; set; } = 0;
    }
}
