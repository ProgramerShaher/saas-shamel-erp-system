namespace ERPsystem.Domain.Enums.Sales
{
    /// <summary>طرق الدفع المختلفة (كاش، بطاقة، حوالة الخ)</summary>
    public enum PaymentMethod 
    { 
        Cash, 
        CreditCard, 
        DebitCard, 
        BankTransfer, 
        Cheque, 
        LoyaltyPoints, 
        Mixed 
    }
}
