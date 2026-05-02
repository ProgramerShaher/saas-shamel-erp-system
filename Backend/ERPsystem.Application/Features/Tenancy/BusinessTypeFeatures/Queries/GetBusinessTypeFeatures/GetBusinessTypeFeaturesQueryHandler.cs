using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Queries.GetBusinessTypeFeatures
{
    /// <summary>
    /// معالج طلب جلب ميزات الأنشطة التجارية.
    /// يقرأ من جدول القوالب في قاعدة البيانات لضمان المرونة.
    /// </summary>
    public class GetBusinessTypeFeaturesQueryHandler : IRequestHandler<GetBusinessTypeFeaturesQuery, BaseResponse<List<BusinessTypeFeatureDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBusinessTypeFeaturesQueryHandler> _logger;

        public GetBusinessTypeFeaturesQueryHandler(
            IApplicationDbContext context, 
            IMapper mapper, 
            ILogger<GetBusinessTypeFeaturesQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<List<BusinessTypeFeatureDto>>> Handle(
            GetBusinessTypeFeaturesQuery request, 
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ جلب ميزات النشاط التجاري: [{BusinessType}]", request.BusinessType);

            var features = await _context.BusinessTypeFeatures
                .Where(x => x.BusinessType == request.BusinessType)
                .ProjectTo<BusinessTypeFeatureDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("✔ تم العثور على [{Count}] ميزات لهذا النشاط.", features.Count);

            return new BaseResponse<List<BusinessTypeFeatureDto>>(features);
        }
    }

    /// <summary>
    /// ملف تعريف الربط (AutoMapper Profile) الخاص بهذه الميزة.
    /// </summary>
    public class GetBusinessTypeFeaturesMappingProfile : Profile
    {
        public GetBusinessTypeFeaturesMappingProfile()
        {
            CreateMap<BusinessTypeFeature, BusinessTypeFeatureDto>()
                .ForMember(d => d.FeatureKey, opt => opt.MapFrom(s => s.FeatureKey.ToString()))
                .ForMember(d => d.FeatureName, opt => opt.MapFrom(s => s.FeatureKey.ToString())); 
                // ملاحظة: يمكن تحسين FeatureName لاحقاً بجلب الوصف من الـ Enum
        }
    }
}
