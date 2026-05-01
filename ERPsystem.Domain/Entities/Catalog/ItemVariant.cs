using System;
using System.Collections.Generic;
using ERPsystem.Domain.Common;

namespace ERPsystem.Domain.Entities.Catalog
{
    /// <summary>
    /// الصنف المتغير/المركب النهائي — مثال لقميص أبيض مقاس L.
    /// يعامل هذا المتغير في المخزن والبيع كصنف مستقل يملِك سعره وتكلفته وباركوده الخاص ولكن يندرج تحت الصنف الرئيسي.
    /// </summary>
    public class ItemVariant : TenantBaseEntity
    {
        /// <summary>الصنف الرئيسي الجامع (والذي غالبا ليس له وجود فيزيائي مستقل في المخزن إلا بمتغيراته)</summary>
        public Guid ItemId { get; set; }
        
        /// <summary>الصنف الرئيسي</summary>
        public virtual Item Item { get; set; } = null!;

        /// <summary>الرمز الشريطي المخزني أو كود الموديل التجاري (SKU) الذي يميز هذه العينة حصريا</summary>
        public string SKU { get; set; } = null!;

        /// <summary>سعر التكلفة الاستثنائي لهذا المقاس أو اللون بالذات</summary>
        public decimal? CostPrice { get; set; }
        
        /// <summary>سعر البيع النهائي لتلك النسخة من الجاكيت</summary>
        public decimal? SalePrice { get; set; }
        
        /// <summary>صورة المتغير (صورة الفستان باللون الأحمر تحديداً)</summary>
        public string? ImageUrl { get; set; }
        
        /// <summary>حالة الموديل وهل ما زال جاري إنتاجه</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>قائمة ارتباط هذا المنتج النهائي بكل محور ورمزه المقابل (أبيض + حرف L)</summary>
        public virtual ICollection<ItemVariantAttributeValue> AttributeValues { get; set; } = new List<ItemVariantAttributeValue>();
        
        /// <summary>باركودات مخصصة لهذه النسخة المحددة بمقاسها ولونها</summary>
        public virtual ICollection<ItemBarcode> Barcodes { get; set; } = new List<ItemBarcode>();
    }
}
