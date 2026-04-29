using System;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities
{
    /// <summary>
    /// يمثل وحدة قياس المنتجات (كجم، لتر، حبة، صندوق).
    /// </summary>
    public class Unit : TenantBaseEntity
    {
        /// <summary>اسم الوحدة كـ كجم أو قطعة</summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>الرمز المختصر للوحدة مثل kg / pcs / L</summary>
        public string Symbol { get; set; } = string.Empty;
    }
}
