namespace ERPsystem.Domain.Enums.Purchasing
{
    /// <summary>حالة تعميد أمر الشراء الموجه للمورد</summary>
    public enum PurchaseOrderStatus 
    { 
        Draft, 
        Sent, 
        PartiallyReceived, 
        FullyReceived, 
        Cancelled 
    }
}
