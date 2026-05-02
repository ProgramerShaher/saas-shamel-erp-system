using System;
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
        /// <summary>معرف الكيان المتعلق بالعملية (لتوحيد الاستجابة في كل أجزاء التطبيق)</summary>
        public Guid? Id { get; set; }

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

            // محاولة استخراج الـ ID تلقائياً لتوحيد شكل الاستجابة للـ Frontend (حسب الطلب)
            if (data is Guid guidId)
            {
                Id = guidId;
            }
            else if (data != null)
            {
                var idProperty = data.GetType().GetProperty("Id");
                if (idProperty != null && idProperty.PropertyType == typeof(Guid))
                {
                    Id = (Guid?)idProperty.GetValue(data);
                }
            }
        }

        public BaseResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
