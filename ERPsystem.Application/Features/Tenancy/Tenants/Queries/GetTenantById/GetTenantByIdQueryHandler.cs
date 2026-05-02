using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById
{
    /// <summary>
    /// معالج استعلام جلب منشأة بواسطة المعرف.
    /// يستخدم ProjectTo لجلب ما هو مطلوب فقط من قاعدة البيانات مباشرة.
    /// </summary>
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, BaseResponse<TenantDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTenantByIdQueryHandler> _logger;

        public GetTenantByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetTenantByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<TenantDto>> Handle(
            GetTenantByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ استعلام جلب المنشأة بالمعرف: [{Id}]", request.Id);

            var tenant = await _context.Tenants
                .AsNoTracking()
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (tenant == null)
            {
                _logger.LogWarning("⚠ لم يتم العثور على المنشأة بالمعرف: [{Id}]", request.Id);
                return new BaseResponse<TenantDto>($"المنشأة غير موجودة بالمعرف الممرر.");
            }

            _logger.LogInformation("✔ تم جلب المنشأة بنجاح | المعرف: [{Id}]", tenant.Id);
            return new BaseResponse<TenantDto>(tenant);
        }
    }
}
