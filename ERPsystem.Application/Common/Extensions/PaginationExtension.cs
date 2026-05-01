using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ملاحظة: يتطلب هذا الملف تسطيب حزمة Microsoft.EntityFrameworkCore في طبقة ה־Application أو עمل Abstraction.
// سأضع هنا كود الـ Extension المشهور الذي يحول الـ IQueryable إلى PagedResponse فوراً.

namespace ERPsystem.Application.Common.Extensions
{
    /// <summary>
    /// دوال مساعدة مشتركة لدعم عمل الـ Pagination بأسلوب احترافي وسريع الكود.
    /// </summary>
    public static class PaginationExtension
    {
        /// <summary>
        /// تحويل استعلام قاعدة البيانات بشكل مباشر إلى PagedResponse
        /// </summary>
        public static async Task<Models.PagedResponse<TDestination>> ToPagedResponseAsync<TDestination>(
            this IQueryable<TDestination> queryable,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default) where TDestination : class
        {
            var count = await queryable.CountAsync(cancellationToken);

            var items = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Models.PagedResponse<TDestination>(items, count, pageNumber, pageSize);
        }
    }
}
