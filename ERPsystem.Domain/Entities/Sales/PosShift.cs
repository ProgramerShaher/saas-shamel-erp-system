using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Sales;

namespace ERPsystem.Domain.Entities.Sales
{
    /// <summary>
    /// وردية وعمل صندوق الكاشير اليومية (Shift).
    /// لا يستطيع أي بائع بيع فاتورة إلا ولديه وردية مفتوحة.
    /// تُستخدم لتقفيل النقدية وعد المطابقة نهاية اليوم.
    /// </summary>
    public class PosShift : TenantBaseEntity
    {
        /// <summary>الرقم المرجعي للوردية</summary>
        public string ShiftNumber { get; set; } = null!;

        /// <summary>نقطة البيع أو الجهاز المفتوح حالياً</summary>
        public Guid PosTerminalId { get; set; }
        
        /// <summary>نقطة البيع</summary>
        public virtual Organization.PosTerminal PosTerminal { get; set; } = null!;

        /// <summary>الكاشير أو المستخدم الموكل إليه الصندوق</summary>
        public Guid CashierUserId { get; set; }

        /// <summary>زمن بداية الوردية</summary>
        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>الزمن الذي أغلقت فيه الوردية من قبل درج الكاشير</summary>
        public DateTime? ClosedAt { get; set; }

        /// <summary>رصيد النقدية الافتتاحي في درج الكاشير للفكّة وغيرها قبل بدء البيع</summary>
        public decimal OpeningCashBalance { get; set; }
        
        /// <summary>الرصيد النقدي المتوقع كما في تقارير النظام من بيع وسلف</summary>
        public decimal ExpectedCashBalance { get; set; }
        
        /// <summary>رصيد النقدية الفعلي المحصى والمعدود باليد عند تسليم الوردية</summary>
        public decimal? ActualCashBalance { get; set; }
        
        /// <summary>فرق التسوية إن وجد (نقص عجز أو زيادة غير مبررة للتحقيق)</summary>
        public decimal? DifferenceValue => ActualCashBalance.HasValue ? ActualCashBalance - ExpectedCashBalance : null;

        /// <summary>حالة الوردية من حيث الاستخدام والإغلاق والتسوية</summary>
        public ShiftStatus Status { get; set; } = ShiftStatus.Open;

        /// <summary>ملاحظات مدير الفرع عند التقفيل</summary>
        public string? ClosingNotes { get; set; }

        /// <summary>جميع الفواتير والمبيعات التي أُصدرت داخل هذه الوردية</summary>
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
    }
}
