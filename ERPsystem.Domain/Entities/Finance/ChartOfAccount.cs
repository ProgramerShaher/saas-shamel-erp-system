using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Finance;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// شجرة الحسابات والدليل المحاسبي (Chart of Accounts - COA).
    /// كل حساب رئيسي وفرعي يضاف هنا لينعكس في الميزانية العمومية وقائمة الدخل.
    /// </summary>
    public class ChartOfAccount : TenantBaseEntity
    {
        /// <summary>رمز أو رقم الحساب المحاسبي (مثال: 101001 للأصول المتداولة)</summary>
        public string AccountCode { get; set; } = null!;
        
        /// <summary>اسم الحساب الفعلي (مثال: نقدية بالصندوق، مبيعات التجزئة)</summary>
        public string AccountName { get; set; } = null!;

        /// <summary>معرف الحساب الأب لتأسيس التفرع الهرمي اللانهائي</summary>
        public Guid? ParentAccountId { get; set; }
        
        /// <summary>الحساب الرئيسي التابع له</summary>
        public virtual ChartOfAccount? ParentAccount { get; set; }

        /// <summary>نوع الحساب الدفتري العام (أصول، خصوم، إيرادات، مصروفات، حقوق ملكية)</summary>
        public AccountType AccountType { get; set; }
        
        /// <summary>التصنيف المالي الدقيق للحساب (نقد، مخزون، أصول ثابتة)</summary>
        public AccountSubType AccountSubType { get; set; }
        
        /// <summary>طبيعة الحساب المعتادة ليتم الحسم (مدين أو دائن)</summary>
        public NormalBalance NormalBalance { get; set; }

        /// <summary>هل هذا الحساب بمثابة مجمع أو مجلد وحسب (تجميعي ولا يقبل حركات مباشرة)؟</summary>
        public bool IsControlAccount { get; set; } = false;

        /// <summary>رصيد الحساب الحالي وفقا للعملة المحلية (ينبني من جمع القيود المكتملة)</summary>
        public decimal CurrentBalance { get; set; } = 0;
        
        /// <summary>هل الحساب قابل للاستلام والصرف</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>الحسابات الفرعية المشتقة من هذا الحساب</summary>
        public virtual ICollection<ChartOfAccount> SubAccounts { get; set; } = new List<ChartOfAccount>();
        
        /// <summary>أسطر قيود اليومية التي استهدفت هذا الحساب حصريا</summary>
        public virtual ICollection<JournalEntryLine> JournalLines { get; set; } = new List<JournalEntryLine>();
    }
}
