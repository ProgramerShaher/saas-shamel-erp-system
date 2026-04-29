using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل حساب في الدليل المحاسبي وشجرة الحسابات العامة (أصول، خصوم، إيرادات، التزامات، حقوق ملكية).
    /// </summary>
    public class ChartOfAccount : TenantBaseEntity
    {
        /// <summary>رمز الحساب في الشجرة المحاسبية (مثل 1100 للأصول)</summary>
        public string Code { get; set; } = string.Empty;   
        
        /// <summary>اسم الحساب (مثل الصندوق الرئيسي، أو البنك المجلي)</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>نوع الحساب الأساسي (Asset, Liability, Equity, Revenue, Expense)</summary>
        public string AccountType { get; set; } = string.Empty; 
        
        /// <summary>طبيعة الحساب العادية هل هي دائن أم مدين (Debit / Credit)</summary>
        public string NormalBalance { get; set; } = "Debit";    
        
        /// <summary>يشير ما إذا كان هذا حساب تجمع رئيسي وليس حساب فرعي للقيود</summary>
        public bool IsParent { get; set; }
        
        /// <summary>معرف الحساب الأب لتمثيل الشجرة والتسلسل الهرمي</summary>
        public Guid? ParentAccountId { get; set; }  
        
        /// <summary>مستوى الحساب في شجرة الحسابات (1 للرئيسية ثم 2 وما يليه)</summary>
        public int Level { get; set; }
        
        /// <summary>حالة تفعيل الحساب للاستخدام</summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>هل يسمح للحساب بإدخال قيود يدوية مباشرة إليه؟</summary>
        public bool AllowManualEntry { get; set; } = true;

        // Navigation (Self-Referencing)
        public virtual ChartOfAccount? ParentAccount { get; set; }
        public virtual ICollection<ChartOfAccount> ChildAccounts { get; set; } = new List<ChartOfAccount>();
        public virtual ICollection<JournalEntryLine> JournalLines { get; set; } = new List<JournalEntryLine>();
    }
}
