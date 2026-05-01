using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// سطر التوزيع داخل سند القبض / الدفع.
    /// يسمح بسداد مصاريف متعددة من حسابات مختلفة في سند دفع واحد.
    /// </summary>
    public class VoucherLine : TenantBaseEntity
    {
        /// <summary>ارتباط السند الأم</summary>
        public Guid VoucherId { get; set; }
        
        /// <summary>السند</summary>
        public virtual Voucher Voucher { get; set; } = null!;

        /// <summary>الحساب المحاسبي المراد تحميل المبلغ عليه (حساب مصروف كهرباء، سلف مستخدمين...)</summary>
        public Guid AccountId { get; set; }
        
        /// <summary>الحساب المستثمر</summary>
        public virtual ChartOfAccount Account { get; set; } = null!;

        /// <summary>مركز التكلفة للمشاركة في العتبة</summary>
        public Guid? CostCenterId { get; set; }

        /// <summary>القيمة المفرزة لهذا السطر من مجمل السند</summary>
        public decimal Amount { get; set; }

        /// <summary>بيان الصرف أو القبض المصغر</summary>
        public string? Description { get; set; }
    }
}
