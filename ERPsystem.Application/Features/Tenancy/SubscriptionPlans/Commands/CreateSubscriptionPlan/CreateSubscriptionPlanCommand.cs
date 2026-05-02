using System;
using ERPsystem.Application.Common.Models;
using MediatR;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    /// <summary>
    /// أمر إنشاء باقة اشتراك جديدة.
    /// </summary>
    public class CreateSubscriptionPlanCommand : IRequest<BaseResponse<Guid>>
    {
        public string PlanName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal? AnnualPrice { get; set; }
        public int MaxBranches { get; set; }
        public int MaxPosTerminals { get; set; }
        public int MaxUsers { get; set; }
        public int MaxItems { get; set; }
        public string AllowedModulesJson { get; set; } = "[]";
        public bool IsActive { get; set; } = true;
    }
}
