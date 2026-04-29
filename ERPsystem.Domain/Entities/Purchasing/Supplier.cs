using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل المورد الذي يتم شراء البضاعة والمنتجات منه.
    /// </summary>
    public class Supplier : TenantBaseEntity
    {
        /// <summary>اسم المورد او الشريك المورد</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>رقم اتصال المورد</summary>
        public string? Phone { get; set; }
        
        /// <summary>البريد الإلكتروني للمورد</summary>
        public string? Email { get; set; }
        
        /// <summary>مقر المورد أو عنوانه</summary>
        public string? Address { get; set; }
        
        /// <summary>الرقم الضريبي للمورد</summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>رصيد المستحقات المالية الباقية لنا او علينا للمورد</summary>
        public decimal Balance { get; set; }

        // Navigation
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
        public virtual ICollection<SupplierPayment> Payments { get; set; } = new List<SupplierPayment>();
    }
}
