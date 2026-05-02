using System;
using System.Security.Claims;
using ERPsystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ERPsystem.API.Services
{
    /// <summary>
    /// خدمة جلب بيانات المستخدم الحالي من الـ HttpContext (JWT Token).
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // جلب معرف المستخدم من الـ Claim الفريد (sub)
        public Guid UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        // جلب معرف المنشأة من الـ Custom Claim (tenant_id)
        public Guid TenantId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value;
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        // التحقق مما إذا كان المستخدم مسجل دخول
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
