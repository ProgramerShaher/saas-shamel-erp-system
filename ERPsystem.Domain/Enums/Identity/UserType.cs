namespace ERPsystem.Domain.Enums.Identity
{
    /// <summary>نوع ونطاق المستخدم (مدير أعلى، مدير فرع، محاسب، كاشير الخ)</summary>
    public enum UserType 
    { 
        SuperAdmin, 
        TenantAdmin, 
        BranchManager, 
        Accountant, 
        Cashier, 
        StoreKeeper, 
        SalesPerson 
    }
}
