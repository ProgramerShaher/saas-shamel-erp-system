using System;
using System.Collections.Generic;

namespace ERPsystem.Application.Common.Models
{
    /// <summary>
    /// غلاف الرد الموحد للقوائم المفلترة (Pagination Response).
    /// يرث من الرد الأساسي ويضيف عليه معلومات الصفحات مثل CurrentPage وغيرها.
    /// </summary>
    /// <typeparam name="T">نوع الكائن المرجوع في القائمة</typeparam>
    public class PagedResponse<T> : BaseResponse<IEnumerable<T>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        
        /// <summary>هل يوجد صفحة تالية؟</summary>
        public bool HasNextPage => CurrentPage < TotalPages;
        
        /// <summary>هل يوجد صفحة سابقة؟</summary>
        public bool HasPreviousPage => CurrentPage > 1;

        public PagedResponse(IEnumerable<T> data, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalRecords = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}
