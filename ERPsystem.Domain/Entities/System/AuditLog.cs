using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يخزن سجلات التدقيق لتتبع أي إضافة، تعديل أو حذف على الكيانات في النظام ومن قبل مَن.
    /// </summary>
    public class AuditLog : TenantBaseEntity
    {
        /// <summary>اسم الجدول أو الكيان الذي تعرّض للحدث</summary>
        public string TableName { get; set; } = string.Empty;
        
        /// <summary>المعرف الفريد الخاص بالسجل داخل ذلك الجدول الذي تم التعديل عليه</summary>
        public Guid RecordId { get; set; }
        
        /// <summary>نوع الإجراء (Create/Update/Delete)</summary>
        public string Action { get; set; } = string.Empty; 
        
        /// <summary>القيم القديمة بصيغة JSON لتسهيل استرجاع البيانات ومعرفة التغييرات قبل التعديل</summary>
        public string? OldValues { get; set; } 
        
        /// <summary>القيم الجديدة بصيغة JSON بعد التعديل</summary>
        public string? NewValues { get; set; } 
        
        /// <summary>عنوان الآي بي لجهاز المستخدم الذي قام بالفعل</summary>
        public string? IpAddress { get; set; }
        
        /// <summary>وقت حفظ سجل التدقيق في النظام</summary>
        public DateTime Timestamp { get; set; }
    }
}
