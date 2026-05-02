namespace ERPsystem.Domain.Enums.Inventory
{
    /// <summary>حالة أمر النقل المخزني بين الفروع</summary>
    public enum TransferStatus 
    { 
        Draft, 
        Approved, 
        Sent, 
        PartiallyReceived, 
        Received, 
        Cancelled 
    }
}
