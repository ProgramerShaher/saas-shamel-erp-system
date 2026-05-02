namespace ERPsystem.Domain.Enums.Inventory
{
    /// <summary>نوع التسوية الجردية (جرد فعلي، إتلاف، عجز، رصيد افتتاحي)</summary>
    public enum AdjustmentType 
    { 
        PhysicalCount, 
        DamageWriteOff, 
        ExpiryWriteOff, 
        OpeningBalance 
    }
}
