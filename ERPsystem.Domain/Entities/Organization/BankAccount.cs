using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Organization
{
    /// <summary>
    /// الحسابات البنكية للمنشأة والتي تستخدم لاستلام مدفوعات البطاقات البنكية والتحويلات أو دفع الموردين.
    /// </summary>
    public class BankAccount : TenantBaseEntity
    {
        /// <summary>معرف الفرع المرتبط به هذا الحساب (غالبا يمكن أن يكون للمركز الرئيسي فيترك Null للكل)</summary>
        public Guid? BranchId { get; set; }
        
        /// <summary>الفرع المحتمل لفرز تقارير البنك</summary>
        public virtual Branch? Branch { get; set; }

        /// <summary>اسم البنك المصرفي (مثال: بنك الإنماء، مصرف الراجحي)</summary>
        public string BankName { get; set; } = null!;
        
        /// <summary>اسم المستفيد المقيد في البنك (غالبا اسم المؤسسة)</summary>
        public string AccountName { get; set; } = null!;
        
        /// <summary>رقم الحساب التعريفي القصير</summary>
        public string AccountNumber { get; set; } = null!;
        
        /// <summary>رقم الحساب المصرفي الدولي IBAN لاستخدامه في الفواتير الضريبية</summary>
        public string? IBAN { get; set; }
        
        /// <summary>رمز الاتصال الدولي السويفت كود للتحويلات الخارجية للسحب والإيداع</summary>
        public string? SwiftCode { get; set; }
        
        /// <summary>عملة الحساب البنكي هذا (تكون حاسمة في تسجيل فوارق سعر الصرف)</summary>
        public string CurrencyCode { get; set; } = "SAR";

        /// <summary>المعرف المحاسبي المرتبط بالحساب (GlAccountId) لتوليد قيود تسوية البنك والسندات البنكية</summary>
        public Guid GlAccountId { get; set; }

        /// <summary>حالة العمل للحساب</summary>
        public bool IsActive { get; set; } = true;
    }
}
