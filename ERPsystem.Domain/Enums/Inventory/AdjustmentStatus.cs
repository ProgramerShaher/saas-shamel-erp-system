namespace ERPsystem.Domain.Enums.Inventory
{
    /// <summary>حالة أمر التسوية الجردية للاعتماد من المدير</summary>
    public enum AdjustmentStatus 
    { 
        Draft, 
        PendingApproval, 
        Approved, 
        Posted, 
        Rejected 
    }
}
