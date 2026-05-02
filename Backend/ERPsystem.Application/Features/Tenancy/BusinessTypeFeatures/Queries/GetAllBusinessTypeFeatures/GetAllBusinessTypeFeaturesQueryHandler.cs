using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetAllBusinessTypeFeatures
{
    /// <summary>
    /// معالج طلب جلب جميع قوالب الميزات.
    /// يطبق قاعدة سادساً للـ Logging باللغة العربية.
    /// </summary>
    public class GetAllBusinessTypeFeaturesQueryHandler : IRequestHandler<GetAllBusinessTypeFeaturesQuery, BaseResponse<List<AllBusinessTypeFeaturesDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetAllBusinessTypeFeaturesQueryHandler> _logger;

        public GetAllBusinessTypeFeaturesQueryHandler(IApplicationDbContext context, ILogger<GetAllBusinessTypeFeaturesQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<List<AllBusinessTypeFeaturesDto>>> Handle(GetAllBusinessTypeFeaturesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ جلب جميع قوالب الميزات للنظام.");

            var features = await _context.BusinessTypeFeatures
                .Select(x => new AllBusinessTypeFeaturesDto
                {
                    BusinessType = x.BusinessType,
                    BusinessTypeName = x.BusinessType.ToString(),
                    FeatureKey = x.FeatureKey.ToString(),
                    IsEnabledByDefault = x.IsEnabledByDefault
                })
                .OrderBy(x => x.BusinessType)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("✔ تم جلب [{Count}] سجل ميزات بنجاح.", features.Count);

            return new BaseResponse<List<AllBusinessTypeFeaturesDto>>(features);
        }
    }
}
