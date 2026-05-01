namespace ERPsystem.Domain.Enums.Tenancy
{
    /// <summary>نوع تغيير الاشتراك (ترقية، تخفيض، تجديد، إغلاق)</summary>
    public enum SubscriptionChangeType 
    { 
        Upgrade, 
        Downgrade, 
        Renew, 
        Cancel, 
        Suspend 
    }
}
