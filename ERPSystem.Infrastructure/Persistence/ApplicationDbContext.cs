using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Domain.Common;
using ERPsystem.Domain.Entities.Catalog;
using ERPsystem.Domain.Entities.Configuration;
using ERPsystem.Domain.Entities.Finance;
using ERPsystem.Domain.Entities.Identity;
using ERPsystem.Domain.Entities.Inventory;
using ERPsystem.Domain.Entities.Organization;
using ERPsystem.Domain.Entities.Purchasing;
using ERPsystem.Domain.Entities.Sales;
using ERPsystem.Domain.Entities.Tenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace ERPsystem.Infrastructure.Persistence
{
    /// <summary>
    /// قلب قاعدة البيانات — يتصل بـ SQL Server ويطبق:
    ///   1. الحذف الناعم (Soft Delete) تلقائياً عبر Override لـ SaveChangesAsync.
    ///   2. تعبئة حقول التتبع (CreatedAt, UpdatedAt) آلياً.
    ///   3. فرض TenantId من ICurrentUserService دون أي تمرير يدوي.
    ///   4. Global Query Filters لإخفاء المحذوفات وبيانات المنشآت الأخرى.
    ///   5. تحميل جميع IEntityTypeConfiguration تلقائياً عبر Reflection.
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        // ══════════════════════════════════════════════════════
        //  Tenancy Module
        // ══════════════════════════════════════════════════════
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<TenantFeatureFlag> TenantFeatureFlags => Set<TenantFeatureFlag>();
        public DbSet<TenantSubscriptionHistory> TenantSubscriptionHistories => Set<TenantSubscriptionHistory>();
        public DbSet<BusinessTypeFeature> BusinessTypeFeatures => Set<BusinessTypeFeature>();

        // ══════════════════════════════════════════════════════
        //  Organization Module
        // ══════════════════════════════════════════════════════
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Headquarters> Headquarters => Set<Headquarters>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<CashSafe> CashSafes => Set<CashSafe>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<PosTerminal> PosTerminals => Set<PosTerminal>();

        // ══════════════════════════════════════════════════════
        //  Identity Module
        // ══════════════════════════════════════════════════════
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();

        // ══════════════════════════════════════════════════════
        //  Catalog Module
        // ══════════════════════════════════════════════════════
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();
        public DbSet<TaxGroup> TaxGroups => Set<TaxGroup>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ItemBarcode> ItemBarcodes => Set<ItemBarcode>();
        public DbSet<ItemUnit> ItemUnits => Set<ItemUnit>();
        public DbSet<ItemVariant> ItemVariants => Set<ItemVariant>();
        public DbSet<VariantAttribute> VariantAttributes => Set<VariantAttribute>();
        public DbSet<VariantAttributeValue> VariantAttributeValues => Set<VariantAttributeValue>();
        public DbSet<ItemVariantAttributeValue> ItemVariantAttributeValues => Set<ItemVariantAttributeValue>();
        public DbSet<PriceList> PriceLists => Set<PriceList>();
        public DbSet<ItemPrice> ItemPrices => Set<ItemPrice>();
        public DbSet<Discount> Discounts => Set<Discount>();

        // ══════════════════════════════════════════════════════
        //  Inventory Module
        // ══════════════════════════════════════════════════════
        public DbSet<StockLedger> StockLedgers => Set<StockLedger>();
        public DbSet<WarehouseStock> WarehouseStocks => Set<WarehouseStock>();
        public DbSet<BatchLot> BatchLots => Set<BatchLot>();
        public DbSet<SerialNumberRecord> SerialNumberRecords => Set<SerialNumberRecord>();
        public DbSet<StockTransferOrder> StockTransferOrders => Set<StockTransferOrder>();
        public DbSet<StockTransferLine> StockTransferLines => Set<StockTransferLine>();
        public DbSet<InventoryAdjustment> InventoryAdjustments => Set<InventoryAdjustment>();
        public DbSet<InventoryAdjustmentLine> InventoryAdjustmentLines => Set<InventoryAdjustmentLine>();

        // ══════════════════════════════════════════════════════
        //  Purchasing Module
        // ══════════════════════════════════════════════════════
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
        public DbSet<PurchaseInvoice> PurchaseInvoices => Set<PurchaseInvoice>();
        public DbSet<PurchaseInvoiceLine> PurchaseInvoiceLines => Set<PurchaseInvoiceLine>();
        public DbSet<PurchaseReturn> PurchaseReturns => Set<PurchaseReturn>();
        public DbSet<PurchaseReturnLine> PurchaseReturnLines => Set<PurchaseReturnLine>();

        // ══════════════════════════════════════════════════════
        //  Sales Module
        // ══════════════════════════════════════════════════════
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<PosShift> PosShifts => Set<PosShift>();
        public DbSet<SalesInvoice> SalesInvoices => Set<SalesInvoice>();
        public DbSet<SalesInvoiceLine> SalesInvoiceLines => Set<SalesInvoiceLine>();
        public DbSet<SalesReturn> SalesReturns => Set<SalesReturn>();
        public DbSet<SalesReturnLine> SalesReturnLines => Set<SalesReturnLine>();
        public DbSet<Payment> Payments => Set<Payment>();

        // ══════════════════════════════════════════════════════
        //  Finance Module
        // ══════════════════════════════════════════════════════
        public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();
        public DbSet<FiscalYear> FiscalYears => Set<FiscalYear>();
        public DbSet<AccountingPeriod> AccountingPeriods => Set<AccountingPeriod>();
        public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
        public DbSet<JournalEntryLine> JournalEntryLines => Set<JournalEntryLine>();
        public DbSet<Voucher> Vouchers => Set<Voucher>();
        public DbSet<VoucherLine> VoucherLines => Set<VoucherLine>();

        // ══════════════════════════════════════════════════════
        //  Configuration Module
        // ══════════════════════════════════════════════════════
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<NumberSequence> NumberSequences => Set<NumberSequence>();
        public DbSet<PrintTemplate> PrintTemplates => Set<PrintTemplate>();
        public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

        // ══════════════════════════════════════════════════════
        //  Override SaveChangesAsync — قواعد الحفظ الآمن
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// يعترض عملية الحفظ ليطبق:
        ///   - تعبئة CreatedAt/CreatedBy عند الإضافة.
        ///   - تعبئة UpdatedAt/UpdatedBy عند التعديل.
        ///   - تحويل Delete إلى Soft Delete (IsDeleted=true).
        ///   - فرض TenantId تلقائياً من الـ JWT بدون تمرير يدوي.
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId   = _currentUserService.UserId;
            var tenantId = _currentUserService.TenantId;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.IsDeleted = false;

                        // فرض TenantId تلقائياً على كيانات TenantBaseEntity
                        if (entry.Entity is TenantBaseEntity tenantEntity && tenantId != Guid.Empty)
                            tenantEntity.TenantId = tenantId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId;
                        break;

                    case EntityState.Deleted:
                        // تحويل الحذف المادي إلى حذف ناعم
                        entry.State             = EntityState.Modified;
                        entry.Entity.IsDeleted  = true;
                        entry.Entity.DeletedAt  = DateTime.UtcNow;
                        entry.Entity.DeletedBy  = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // تحميل جميع IEntityTypeConfiguration تلقائياً من الـ Assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            // جلب كل العلاقات في قاعدة البيانات
            var foreignKeys = builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys());

            foreach (var fk in foreignKeys)
            {
                // تغيير سلوك الحذف من Cascade إلى Restrict أو NoAction
                // Restrict تمنع الحذف إذا كان هناك سجلات مرتبطة
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Global Query Filters: إخفاء المحذوفات + عزل بيانات المنشآت
            ApplyGlobalFilters(builder);
        }

        /// <summary>
        /// يطبق Global Query Filter على جميع الكيانات:
        ///   - TenantBaseEntity: فلتر IsDeleted=false + TenantId
        ///   - BaseEntity: فلتر IsDeleted=false فقط (مثل Tenant نفسه)
        /// </summary>
        private void ApplyGlobalFilters(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (typeof(TenantBaseEntity).IsAssignableFrom(clrType))
                {
                    var param           = Expression.Parameter(clrType, "e");
                    var isDeletedProp   = Expression.Property(param, nameof(BaseEntity.IsDeleted));
                    var notDeleted      = Expression.Equal(isDeletedProp, Expression.Constant(false));

                    var tenantIdProp    = Expression.Property(param, nameof(TenantBaseEntity.TenantId));
                    var svcConst        = Expression.Constant(_currentUserService);
                    var tenantIdVal     = Expression.Property(svcConst, nameof(ICurrentUserService.TenantId));
                    var sameTenant      = Expression.Equal(tenantIdProp, tenantIdVal);

                    var combined        = Expression.AndAlso(notDeleted, sameTenant);
                    builder.Entity(clrType).HasQueryFilter(Expression.Lambda(combined, param));
                }
                else if (typeof(BaseEntity).IsAssignableFrom(clrType))
                {
                    var param           = Expression.Parameter(clrType, "e");
                    var isDeletedProp   = Expression.Property(param, nameof(BaseEntity.IsDeleted));
                    var notDeleted      = Expression.Equal(isDeletedProp, Expression.Constant(false));
                    builder.Entity(clrType).HasQueryFilter(Expression.Lambda(notDeleted, param));
                }
            }
        }
    }
}
