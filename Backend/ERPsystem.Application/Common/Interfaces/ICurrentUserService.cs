using System;

namespace ERPsystem.Application.Common.Interfaces
{
    /// <summary>
    /// مزود خدمة يقدم معلومات هوية المستخدم الحالي وبيئة عمله بناءً على الـ Token.
    /// تطبيقا للقواعد، يتم استخراج TenantId من هنا ولا يتم تمريره يدويا.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>معرف المستخدم الفعلي أو معرف الجلسة الحالية</summary>
        Guid UserId { get; }

        /// <summary>معرف المنشأة (مهم جدا للـ Multi-Tenancy)</summary>
        Guid TenantId { get; }
        
        /// <summary>هل يوجد مستخدم مسجل دخول فعلياً؟</summary>
        bool IsAuthenticated { get; }
    }
}
