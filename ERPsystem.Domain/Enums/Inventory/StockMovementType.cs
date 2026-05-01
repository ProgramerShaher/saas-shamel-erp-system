namespace ERPsystem.Domain.Enums.Inventory
{
    /// <summary>أنواع حركة المخزون في النظام صادر وارد وتالف</summary>
    public enum StockMovementType
    {
        PurchaseIn,
        PurchaseReturnOut,
        SaleOut,
        SaleReturnIn,
        TransferOut,
        TransferIn,
        AdjustmentIn,
        AdjustmentOut,
        OpeningBalance,
        DamageOut
    }
}
