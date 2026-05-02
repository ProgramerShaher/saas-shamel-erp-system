using System;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans
{
    /// <summary>
    /// البيانات المرجعة في القائمة.
    /// </summary>
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public string PlanName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal? AnnualPrice { get; set; }
        public int MaxBranches { get; set; }
        public int MaxPosTerminals { get; set; }
        public int MaxUsers { get; set; }
        public int MaxItems { get; set; }
        public string AllowedModulesJson { get; set; } = "[]";
        public bool IsActive { get; set; }
    }
}
