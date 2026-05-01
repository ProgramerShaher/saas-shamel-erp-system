using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Tenancy
using ERPsystem.Domain.Entities.Tenancy;
// Organization
using ERPsystem.Domain.Entities.Organization;
// Identity
using ERPsystem.Domain.Entities.Identity;
// Catalog
using ERPsystem.Domain.Entities.Catalog;
// Inventory
using ERPsystem.Domain.Entities.Inventory;
// Purchasing
using ERPsystem.Domain.Entities.Purchasing;
// Sales
using ERPsystem.Domain.Entities.Sales;
// Finance
using ERPsystem.Domain.Entities.Finance;
// Configuration
using ERPsystem.Domain.Entities.Configuration;

namespace ERPsystem.Application.Common.Interfaces
{
    /// <summary>
    /// واجهة طبقة البنية التحتية — تُستخدم في الـ Handlers بدون ارتباط مباشر بـ EF Core.
    /// تعكس الكيانات الحقيقية الموجودة في طبقة الـ Domain بالأسماء الصحيحة.
    /// </summary>
    public interface IApplicationDbContext
    {
        // ══════════════════════════════════════════════════════
        //  Tenancy Module
        // ══════════════════════════════════════════════════════
        DbSet<Tenant> Tenants { get; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; }
        DbSet<TenantFeatureFlag> TenantFeatureFlags { get; }
        DbSet<TenantSubscriptionHistory> TenantSubscriptionHistories { get; }

        // ══════════════════════════════════════════════════════
        //  Organization Module
        // ══════════════════════════════════════════════════════
        DbSet<Branch> Branches { get; }
        DbSet<Headquarters> Headquarters { get; }
        DbSet<Warehouse> Warehouses { get; }
        DbSet<CashSafe> CashSafes { get; }
        DbSet<BankAccount> BankAccounts { get; }
        DbSet<PosTerminal> PosTerminals { get; }

        // ══════════════════════════════════════════════════════
        //  Identity Module
        // ══════════════════════════════════════════════════════
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<UserSession> UserSessions { get; }

        // ══════════════════════════════════════════════════════
        //  Catalog Module
        // ══════════════════════════════════════════════════════
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        DbSet<UnitOfMeasure> UnitOfMeasures { get; }
        DbSet<TaxGroup> TaxGroups { get; }
        DbSet<Item> Items { get; }
        DbSet<ItemBarcode> ItemBarcodes { get; }
        DbSet<ItemUnit> ItemUnits { get; }
        DbSet<ItemVariant> ItemVariants { get; }
        DbSet<VariantAttribute> VariantAttributes { get; }
        DbSet<VariantAttributeValue> VariantAttributeValues { get; }
        DbSet<ItemVariantAttributeValue> ItemVariantAttributeValues { get; }
        DbSet<PriceList> PriceLists { get; }
        DbSet<ItemPrice> ItemPrices { get; }
        DbSet<Discount> Discounts { get; }

        // ══════════════════════════════════════════════════════
        //  Inventory Module
        // ══════════════════════════════════════════════════════
        DbSet<StockLedger> StockLedgers { get; }
        DbSet<WarehouseStock> WarehouseStocks { get; }
        DbSet<BatchLot> BatchLots { get; }
        DbSet<SerialNumberRecord> SerialNumberRecords { get; }
        DbSet<StockTransferOrder> StockTransferOrders { get; }
        DbSet<StockTransferLine> StockTransferLines { get; }
        DbSet<InventoryAdjustment> InventoryAdjustments { get; }
        DbSet<InventoryAdjustmentLine> InventoryAdjustmentLines { get; }

        // ══════════════════════════════════════════════════════
        //  Purchasing Module
        // ══════════════════════════════════════════════════════
        DbSet<Supplier> Suppliers { get; }
        DbSet<PurchaseOrder> PurchaseOrders { get; }
        DbSet<PurchaseOrderLine> PurchaseOrderLines { get; }
        DbSet<PurchaseInvoice> PurchaseInvoices { get; }
        DbSet<PurchaseInvoiceLine> PurchaseInvoiceLines { get; }
        DbSet<PurchaseReturn> PurchaseReturns { get; }
        DbSet<PurchaseReturnLine> PurchaseReturnLines { get; }

        // ══════════════════════════════════════════════════════
        //  Sales Module
        // ══════════════════════════════════════════════════════
        DbSet<Customer> Customers { get; }
        DbSet<PosShift> PosShifts { get; }
        DbSet<SalesInvoice> SalesInvoices { get; }
        DbSet<SalesInvoiceLine> SalesInvoiceLines { get; }
        DbSet<SalesReturn> SalesReturns { get; }
        DbSet<SalesReturnLine> SalesReturnLines { get; }
        DbSet<Payment> Payments { get; }

        // ══════════════════════════════════════════════════════
        //  Finance Module
        // ══════════════════════════════════════════════════════
        DbSet<ChartOfAccount> ChartOfAccounts { get; }
        DbSet<FiscalYear> FiscalYears { get; }
        DbSet<AccountingPeriod> AccountingPeriods { get; }
        DbSet<JournalEntry> JournalEntries { get; }
        DbSet<JournalEntryLine> JournalEntryLines { get; }
        DbSet<Voucher> Vouchers { get; }
        DbSet<VoucherLine> VoucherLines { get; }

        // ══════════════════════════════════════════════════════
        //  Configuration Module
        // ══════════════════════════════════════════════════════
        DbSet<AuditLog> AuditLogs { get; }
        DbSet<NumberSequence> NumberSequences { get; }
        DbSet<PrintTemplate> PrintTemplates { get; }
        DbSet<SystemSetting> SystemSettings { get; }

        /// <summary>حفظ التغييرات مع تطبيق Soft Delete وتعبئة الحقول التلقائية</summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
