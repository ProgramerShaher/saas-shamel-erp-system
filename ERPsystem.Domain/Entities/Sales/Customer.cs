using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// العميل أو الزبون الذي تم بيع البضاعة له أو تقديم الخدمة إليه.
    /// </summary>
    public class Customer : TenantBaseEntity
    {
        /// <summary>اسم العميل</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>رقم الهاتف للتواصل</summary>
        public string? Phone { get; set; }
        
        /// <summary>البريد الإلكتروني للعميل</summary>
        public string? Email { get; set; }
        
        /// <summary>عنوان العميل</summary>
        public string? Address { get; set; }
        
        /// <summary>الحد الائتماني لمديونية العميل (أقصى قيمة للدين المسموح)</summary>
        public decimal CreditLimit { get; set; }
        
        /// <summary>الرصيد الحالي للعميل (الإجمالي المتبقي المستحق عليه)</summary>
        public decimal Balance { get; set; }

        // Navigation
        public virtual ICollection<SaleInvoice> SaleInvoices { get; set; } = new List<SaleInvoice>();
        public virtual ICollection<CustomerPayment> Payments { get; set; } = new List<CustomerPayment>();
    }
}
