using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// شرائح أو مجموعات الضريبة (تطبق على الأصناف والفواتير).
    /// كضريبة القيمة المضافة 15%، أو السلع المعفاة والصفرية.
    /// </summary>
    public class TaxGroup : TenantBaseEntity
    {
        /// <summary>اسم فئة الضريبة (مثال: ضريبة القيمة المضافة 15%)</summary>
        public string Name { get; set; } = null!;

        /// <summary>النسبة المئوية المقتطعة للاستقطاع الضريبي</summary>
        public decimal Rate { get; set; }

        /// <summary>هل هي الضريبة الافتراضية لأي منتج جديد يُضاف للعميل؟</summary>
        public bool IsDefault { get; set; } = false;
        
        /// <summary>حالة تفعيل المجموعة الضريبية</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>رقم الحساب المحاسبي الفعلي (الخصوم/الالتزامات) في شجرة الحسابات الذي تُطرح فيه قيمة الضريبة لتوريدها لاحقا للهيئة</summary>
        public Guid GlTaxAccountId { get; set; }
    }
}
