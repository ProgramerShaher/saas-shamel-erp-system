using System.Collections.Generic;

namespace ERPsystem.Application.Common.Models
{
    /// <summary>
    /// الغلاف الأساسي الموحد لجميع الردود (Responses) الصادرة من الـ API.
    /// يوحد شكل الرد بحيث يكون مفهوم للـ Frontend دائماً سواء في حالة النجاح أو الفشل.
    /// </summary>
    /// <typeparam name="T">نوع البيانات المرجعة (مثل TenantDto)</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>هل نجحت العملية أم لا؟</summary>
        public bool Succeeded { get; set; }
        
        /// <summary>رسالة موجهة للمستخدم (تم الحفظ بنجاح، او يوجد خطأ)</summary>
        public string? Message { get; set; }
        
        /// <summary>قائمة تفصيلية بأية أخطاء (Validation Errors مثلا)</summary>
        public List<string>? Errors { get; set; }
        
        /// <summary>البيانات المطلوبة للإرجاع</summary>
        public T? Data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T data, string? message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public BaseResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
