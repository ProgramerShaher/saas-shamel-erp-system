namespace ERPsystem.Domain.Enums.Tenancy
{
    /// <summary>
    /// مفاتيح الميزات المتاحة في النظام (Feature Flags).
    /// تُستخدم لتفعيل أو تعطيل وحدات معينة بحسب نوع نشاط المنشأة (BusinessType) وباقة الاشتراك.
    /// </summary>
    public enum FeatureKey
    {
        // ================= Inventory =================
        EXPIRY_DATES           = 1,
        BATCH_NUMBERS          = 2,
        SERIAL_NUMBERS         = 3,
        MULTI_BARCODE          = 4,
        MULTI_WAREHOUSE        = 5,
        STOCK_TRANSFERS        = 6,
        INVENTORY_ADJUSTMENT   = 7,

        // ================= Catalog =================
        PRODUCT_VARIANTS       = 10,
        MULTI_UNIT             = 11,
        PRICE_LISTS            = 12,

        // ================= Sales & POS =================
        POS_MODULE             = 20,
        WHOLESALE_MODULE       = 21,
        LOYALTY_POINTS         = 22,
        ADVANCED_DISCOUNTS     = 23,
        SALES_ORDERS           = 24,

        // ================= Purchasing =================
        PURCHASE_REQUESTS      = 30,
        PURCHASE_ORDERS        = 31,

        // ================= Finance =================
        COST_CENTERS           = 40,
        MULTI_CURRENCY         = 41,
        BUDGETING              = 42,
        MANUAL_JOURNAL_ENTRIES = 43,

        // ================= Organization =================
        MULTI_BRANCH           = 50,
        HEADQUARTERS_STRUCTURE = 51,

        // ================= Reporting =================
        ADVANCED_REPORTS       = 60,
        EXECUTIVE_DASHBOARD    = 61,

        // ================= Specific Industry =================
        PHARMACY_MODULE        = 70,
        RESTAURANT_MODULE      = 71,
    }
}
