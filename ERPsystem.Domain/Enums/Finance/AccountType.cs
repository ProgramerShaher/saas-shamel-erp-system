namespace ERPsystem.Domain.Enums.Finance
{
    /// <summary>نوع الحساب الرئيسي في شجرة الحسابات (أصول، خصوم، إيرادات، التزامات، حقوق ملكية)</summary>
    public enum AccountType 
    { 
        Asset, 
        Liability, 
        Equity, 
        Revenue, 
        Expense 
    }
}
