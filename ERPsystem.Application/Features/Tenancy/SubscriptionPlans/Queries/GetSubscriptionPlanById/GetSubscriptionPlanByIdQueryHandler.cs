using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    /// <summary>
    /// معالج استعلام استرجاع باقة واحدة.
    /// </summary>
    public class GetSubscriptionPlanByIdQueryHandler : IRequestHandler<GetSubscriptionPlanByIdQuery, BaseResponse<SubscriptionPlanDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSubscriptionPlanByIdQueryHandler> _logger;

        public GetSubscriptionPlanByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetSubscriptionPlanByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<SubscriptionPlanDto>> Handle(GetSubscriptionPlanByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ استعلام باقة اشتراك بالمعرف: [{PlanId}]", request.Id);

            var plan = await _context.SubscriptionPlans
                .AsNoTracking()
                .ProjectTo<SubscriptionPlanDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (plan == null)
            {
                _logger.LogWarning("⚠ لم يتم العثور على الباقة بالمعرف: [{PlanId}]", request.Id);
                return new BaseResponse<SubscriptionPlanDto>("الباقة غير موجودة.");
            }

            _logger.LogInformation("✔ تم جلب الباقة بنجاح | المعرف: [{PlanId}]", plan.Id);
            return new BaseResponse<SubscriptionPlanDto>(plan);
        }
    }
}
