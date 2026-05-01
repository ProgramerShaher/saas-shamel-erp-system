using System;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Sales;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// الزبون أو العميل الذي يشتري السلع.
    /// في التجزئة السريعة قد يكون عميل Walk-in (عميل نقدي بدون تسجيل اسمه).
    /// </summary>
    public class Customer : TenantBaseEntity
    {
        /// <summary>اسم العميل / المؤسسة</summary>
        public string Name { get; set; } = null!;
        
        /// <summary>رقم الجوال لتسجيل الدخول كنقاط ولاء</summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>البريد الإلكتروني للعميل لرسائل المعايدة والفواتير</summary>
        public string? Email { get; set; }
        
        /// <summary>الرقم الضريبي إن كان العميل شركة ويحتاج فاتورة ضريبية رسمية</summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>العنوان الخاص بتوصيل الطلبات</summary>
        public string? Address { get; set; }

        /// <summary>تصنيف شريحة العميل (جملة، تجزئة نقدي، عميل VIP)</summary>
        public CustomerType CustomerType { get; set; } = CustomerType.WalkIn;

        /// <summary>رقم الحساب المحاسبي للعميل (إذا كان يشتري بالآجل كذمم مدينة)</summary>
        public Guid? GlAccountId { get; set; }

        /// <summary>الحد الأقصى المسموح له كآجل (دين)</summary>
        public decimal CreditLimit { get; set; } = 0;
        
        /// <summary>إجمالي الدين الحالي أو المستحق على هذا العميل للنظام</summary>
        public decimal CurrentBalance { get; set; } = 0;

        /// <summary>نقاط المكافآت أو الولاء (Loyalty Points) المجمعة لدى هذا العميل لاستبدالها</summary>
        public int LoyaltyPointsBalance { get; set; } = 0;

        /// <summary>حالة الحساب إما نشط أو محظور للديون</summary>
        public bool IsActive { get; set; } = true;
    }
}
