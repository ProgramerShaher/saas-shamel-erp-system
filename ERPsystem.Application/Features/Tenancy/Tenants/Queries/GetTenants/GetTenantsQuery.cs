using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants
{
    /// <summary>
    /// استعلام استرجاع قائمة المنشآت مع دعم الـ Pagination والبحث.
    /// يُطبق مبدأ CQRS: هذا Query للقراءة فقط ولا يغير البيانات.
    /// </summary>
    public record GetTenantsQuery : IRequest<PagedResponse<TenantVm>>
    {
        /// <summary>رقم الصفحة الحالية</summary>
        public int PageNumber { get; init; } = 1;

        /// <summary>عدد السجلات لكل صفحة</summary>
        public int PageSize { get; init; } = 10;

        /// <summary>كلمة البحث (اختياري)</summary>
        public string? SearchKeyword { get; init; }
    }
}
