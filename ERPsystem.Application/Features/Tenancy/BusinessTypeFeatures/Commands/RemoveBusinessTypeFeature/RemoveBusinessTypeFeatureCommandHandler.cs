using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.RemoveBusinessTypeFeature
{
    /// <summary>
    /// معالج أمر حذف ميزة من نشاط تجاري.
    /// </summary>
    public class RemoveBusinessTypeFeatureCommandHandler : IRequestHandler<RemoveBusinessTypeFeatureCommand, BaseResponse<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<RemoveBusinessTypeFeatureCommandHandler> _logger;

        public RemoveBusinessTypeFeatureCommandHandler(IApplicationDbContext context, ILogger<RemoveBusinessTypeFeatureCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> Handle(RemoveBusinessTypeFeatureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ حذف الميزة [{Feature}] من النشاط [{BusinessType}]", request.FeatureKey, request.BusinessType);

            var entity = await _context.BusinessTypeFeatures
                .FirstOrDefaultAsync(x => x.BusinessType == request.BusinessType && x.FeatureKey == request.FeatureKey, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("⚠ الميزة [{Feature}] غير موجودة أصلاً للنشاط [{BusinessType}]", request.FeatureKey, request.BusinessType);
                return new BaseResponse<bool>("الميزة غير موجودة في سجلات هذا النشاط.");
            }

            _context.BusinessTypeFeatures.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تم حذف الميزة بنجاح من النشاط.");

            return new BaseResponse<bool>(true, "تم حذف الميزة بنجاح.");
        }
    }
}
