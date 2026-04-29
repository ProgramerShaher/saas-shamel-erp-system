using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل المنشأة أو المستأجر في النظام (Tenant). هذا هو الجذر الأساسي لعزل البيانات.
    /// يخزن معلومات الاشتراك وتفاصيل المنشأة التجارية.
    /// </summary>
    public class Tenant : BaseEntity
    {
        /// <summary>اسم المنشأة</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>رابط شعار المنشأة</summary>
        public string? LogoUrl { get; set; }
        
        /// <summary>عنوان المنشأة</summary>
        public string? Address { get; set; }
        
        /// <summary>رقم هاتف المنشأة</summary>
        public string? Phone { get; set; }
        
        /// <summary>البريد الإلكتروني للتواصل</summary>
        public string? Email { get; set; }
        
        /// <summary>الرقم الضريبي العائد للمنشأة (إن وجد)</summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>رمز العملة الافتراضي للمنشأة (مثال: SAR)</summary>
        public string CurrencyCode { get; set; } = "SAR";
        
        /// <summary>تنسيق التاريخ المفضل (مثال: dd/MM/yyyy)</summary>
        public string DateFormat { get; set; } = "dd/MM/yyyy";
        
        /// <summary>البادئة المخصصة للفواتير (مثال: INV)</summary>
        public string InvoicePrefix { get; set; } = "INV";
        
        /// <summary>عداد أو رقم آخر فاتورة تم إصدارها لضمان التسلسل التلقائي</summary>
        public int InvoiceCounter { get; set; } = 1;
        
        /// <summary>نوع نشاط المنشأة (بقالة، صيدلية، الخ)</summary>
        public string BusinessType { get; set; } = string.Empty; 
        
        /// <summary>تاريخ بدء اشتراك المنشأة في النظام</summary>
        public DateTime SubscriptionStart { get; set; }
        
        /// <summary>تاريخ انتهاء اشتراك المنشأة في النظام</summary>
        public DateTime SubscriptionEnd { get; set; }
        
        /// <summary>حالة فعالية المنشأة (إذا كان الحساب موقوفاً أو مفضلاً)</summary>
        public bool IsActive { get; set; } = true;

        // Navigation
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<SaleInvoice> SaleInvoices { get; set; } = new List<SaleInvoice>();
        public virtual ICollection<ChartOfAccount> ChartOfAccounts { get; set; } = new List<ChartOfAccount>();
    }
}
