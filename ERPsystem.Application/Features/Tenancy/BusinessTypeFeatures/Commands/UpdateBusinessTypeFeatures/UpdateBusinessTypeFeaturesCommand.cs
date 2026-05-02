using System.Collections.Generic;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Enums.Tenancy;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.UpdateBusinessTypeFeatures
{
    /// <summary>
    /// أمر تحديث قائمة الميزات الافتراضية لنوع نشاط تجاري معين.
    /// يقوم باستبدال القائمة القديمة بالقائمة الجديدة الممررة.
    /// </summary>
    public class UpdateBusinessTypeFeaturesCommand : IRequest<BaseResponse<bool>>
    {
        /// <summary>نوع النشاط المراد تحديثه</summary>
        public BusinessType BusinessType { get; set; }

        /// <summary>قائمة مفاتيح الميزات المراد تفعيلها لهذا النشاط</summary>
        public List<FeatureKey> FeatureKeys { get; set; } = new();
    }
}
