namespace ERPsystem.Domain.Enums.Shared
{
    /// <summary>حالة الدفع للفواتير المالية (غير مدفوع، مدفوع جزئياً، مدفوع بالكامل)</summary>
    public enum PaymentStatus 
    { 
        Unpaid, 
        PartiallyPaid, 
        FullyPaid, 
        Overpaid 
    }
}
