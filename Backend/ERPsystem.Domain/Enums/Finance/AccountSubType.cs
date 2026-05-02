namespace ERPsystem.Domain.Enums.Finance
{
    /// <summary>النوع الفرعي للحساب (نقد، بنك، ذمم مدينة، مخزون الخ)</summary>
    public enum AccountSubType
    {
        Cash, 
        Bank, 
        AccountsReceivable, 
        Inventory,
        FixedAsset, 
        AccountsPayable, 
        TaxPayable,
        RetainedEarnings, 
        Revenue, 
        CostOfGoodsSold, 
        OperatingExpense
    }
}
