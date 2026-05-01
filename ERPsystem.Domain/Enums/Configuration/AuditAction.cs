namespace ERPsystem.Domain.Enums.Configuration
{
    /// <summary>نوع العملية في سجل التدقيق ومراقبة المشرفين</summary>
    public enum AuditAction 
    { 
        Create, 
        Update, 
        Delete, 
        SoftDelete, 
        Login, 
        Logout, 
        Export 
    }
}
