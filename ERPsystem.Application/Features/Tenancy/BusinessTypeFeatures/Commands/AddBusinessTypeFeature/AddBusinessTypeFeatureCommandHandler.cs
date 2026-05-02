using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Domain.Entities.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.BusinessTypeFeatures.Commands.AddBusinessTypeFeature
{
    /// <summary>
    /// معالج أمر إضافة ميزة لنشاط تجاري.
    /// يطبق التحقق من التكرار والـ Logging العربي.
    /// </summary>
    public class AddBusinessTypeFeatureCommandHandler : IRequestHandler<AddBusinessTypeFeatureCommand, BaseResponse<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AddBusinessTypeFeatureCommandHandler> _logger;

        public AddBusinessTypeFeatureCommandHandler(IApplicationDbContext context, ILogger<AddBusinessTypeFeatureCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Guid>> Handle(AddBusinessTypeFeatureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ إضافة ميزة [{Feature}] للنشاط [{BusinessType}]", request.FeatureKey, request.BusinessType);

            var exists = await _context.BusinessTypeFeatures
                .AnyAsync(x => x.BusinessType == request.BusinessType && x.FeatureKey == request.FeatureKey, cancellationToken);

            if (exists)
            {
                _logger.LogWarning("⚠ الميزة [{Feature}] موجودة بالفعل للنشاط [{BusinessType}]", request.FeatureKey, request.BusinessType);
                return new BaseResponse<Guid>("هذه الميزة مضافة بالفعل لهذا النشاط التجاري.");
            }

            var entity = new BusinessTypeFeature
            {
                BusinessType = request.BusinessType,
                FeatureKey = request.FeatureKey,
                IsEnabledByDefault = true
            };

            await _context.BusinessTypeFeatures.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("✔ تمت إضافة الميزة بنجاح. المعرف: [{Id}]", entity.Id);

            return new BaseResponse<Guid>(entity.Id, "تمت إضافة الميزة بنجاح.");
        }
    }
}
