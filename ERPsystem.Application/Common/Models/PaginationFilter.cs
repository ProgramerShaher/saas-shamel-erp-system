namespace ERPsystem.Application.Common.Models
{
    /// <summary>
    /// كائن الطلب الموحد (Request) لاستقبال رقم وحجم الصفحة من المستخدم في الـ Query.
    /// ترث منه اية Queries تتطلب پاجينيشن في ה־Features.
    /// </summary>
    public class PaginationFilter
    {
        private const int MaxPageSize = 500;
        private int _pageSize = 10;

        /// <summary>رقم الصفحة الحالية (الافتراضي: 1)</summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>عدد السجلات للصفحة الواحدة (الافتراضي: 10، والحد الأقصى: 500 ليمنع الضغط على السيرفر)</summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : (value < 1 ? 1 : value);
        }

        // يمكن إضافة حقول عامة للبحث هنا
        /// <summary>كلمة مفتاحية للبحث المشترك (اختياري)</summary>
        public string? SearchKeyword { get; set; }
    }
}
