using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Enums.Inventory;

namespace ERPsystem.Domain.Entities.Inventory
{
    /// <summary>
    /// أمر التحويل المخزني (Stock Transfer).
    /// لنقل البضاعة بين فروع الشركة أو مستودعاتها بشكل موثق ومرحلي (إرسال، بالطريق، تم الاستلام).
    /// </summary>
    public class StockTransferOrder : TenantBaseEntity
    {
        /// <summary>الرقم التسلسلي الدوري لأمر النقل</summary>
        public string TransferNumber { get; set; } = null!;

        /// <summary>مستودع المنشأ / المصدر المأخوذ منه الشحنة</summary>
        public Guid FromWarehouseId { get; set; }
        
        /// <summary>المستودع المصدر</summary>
        public virtual Organization.Warehouse FromWarehouse { get; set; } = null!;

        /// <summary>مستودع الوجهة والمستقبل للشحنة</summary>
        public Guid ToWarehouseId { get; set; }
        
        /// <summary>المستودع الوجهة</summary>
        public virtual Organization.Warehouse ToWarehouse { get; set; } = null!;

        /// <summary>مرحلة النقل (مسودة، معتمد، مرسل، أو تم الاستلام)</summary>
        public TransferStatus Status { get; set; } = TransferStatus.Draft;

        /// <summary>الموظف الصانع لطلب التحويل المخزني</summary>
        public Guid RequestedByUserId { get; set; }
        
        /// <summary>زمن بداية رفع أمر النقل</summary>
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        /// <summary>المدير المخول الذي اعتمد وأثبت صحة خروج النقل من المصدر</summary>
        public Guid? ApprovedByUserId { get; set; }
        
        /// <summary>زمن تحرك وخروج الشاحنات وبدء النقل فعلياً</summary>
        public DateTime? SentAt { get; set; }
        
        /// <summary>الزمن الذي أقر فيه مسؤول الوجهة انه استلم البضاعة</summary>
        public DateTime? ReceivedAt { get; set; }

        /// <summary>ملاحظات بوليصة الشحن وإجراءات النقل</summary>
        public string? Notes { get; set; }
        
        /// <summary>القيد المحاسبي المرتبط إذا كان النقل بين فرعين مختلفين مالياً</summary>
        public Guid? JournalEntryId { get; set; }

        /// <summary>الأصناف المشحونة ضمن هذه البوليصة وكمياتها المطلوبة والمستلمة</summary>
        public virtual ICollection<StockTransferLine> Lines { get; set; } = new List<StockTransferLine>();
    }
}
