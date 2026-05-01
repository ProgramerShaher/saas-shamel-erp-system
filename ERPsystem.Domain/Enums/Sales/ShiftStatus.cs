namespace ERPsystem.Domain.Enums.Sales
{
    /// <summary>حالة وردية الكاشير (مفتوحة، بانتظار الإغلاق، مغلقة، تمت التسوية)</summary>
    public enum ShiftStatus 
    { 
        Open, 
        PendingClose, 
        Closed, 
        Reconciled 
    }
}
