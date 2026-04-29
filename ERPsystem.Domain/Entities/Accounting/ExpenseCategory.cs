using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل تصنيف المصروفات (رواتب، إيجارات، فواتير كهرباء، صيانة).
    /// </summary>
    public class ExpenseCategory : TenantBaseEntity
    {
        /// <summary>اسم فئة المصروف</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>وصف الفئة</summary>
        public string? Description { get; set; }
        
        /// <summary>معرف الحساب المربوط به في شجرة الحسابات المحاسبية</summary>
        public Guid? AccountId { get; set; } 

        // Navigation
        public virtual ChartOfAccount? Account { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
