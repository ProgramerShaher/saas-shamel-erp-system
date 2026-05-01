using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Finance;

namespace ERPsystem.Domain.Entities.Finance
{
    /// <summary>
    /// سندات القبض والصرف اليدوية (Receipt Voucher / Payment Voucher).
    /// </summary>
    public class Voucher : TenantBaseEntity
    {
        /// <summary>رقم السند</summary>
        public string VoucherNumber { get; set; } = null!;

        /// <summary>نوع السند الحاضر هل هو صرف نقدي (Payment) أما قبض (Receipt)</summary>
        public VoucherType FilterType { get; set; }

        /// <summary>الجهة الموجه السند إما لها أو منها</summary>
        public string PartyName { get; set; } = null!;

        /// <summary>تاريخ السند</summary>
        public DateTime VoucherDate { get; set; } = DateTime.UtcNow;

        /// <summary>الآلية للصرف</summary>
        public ReceiptPaymentMethod Method { get; set; }

        /// <summary>إجمالي القيمة المقبوضة أو المنصرفة</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>حساب الصندوق أو البنك الذي دفع أو تسلم المال</summary>
        public Guid CashOrBankAccountId { get; set; }

        /// <summary>المبرر والعنوان الرئيسي للسند</summary>
        public string? Description { get; set; }

        /// <summary>القيد المحاسبي المتولد لتوثيق السند بالميزانية (من حساب العميل إلى حساب الصندوق)</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>قائمة الترحيل المتعدد للاستفادات المتفرقة داخل السند</summary>
        public virtual ICollection<VoucherLine> Lines { get; set; } = new List<VoucherLine>();
    }

    /// <summary>نوع السند القبض أم الدفع</summary>
    public enum VoucherType { Receipt, Payment }
}
