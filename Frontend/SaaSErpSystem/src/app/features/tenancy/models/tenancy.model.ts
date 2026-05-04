export interface Tenant {
  id: string;
  name: string;
  slug: string;
  businessType: number;
  isActive: boolean;
  logoUrl?: string;
  createdAt: string;
}

export interface CreateTenantCommand {
  name: string;
  slug: string;
  businessType: number;
  subscriptionPlanId: string;
  primaryColor?: string;
  logoUrl?: string;
}

export interface UpdateTenantCommand {
  id: string;
  name: string;
  isActive: boolean;
  businessType: number;
  subscriptionPlanId: string;
  logoUrl?: string;
  primaryColor?: string;
}

export interface SubscriptionPlan {
  id: string;
  planName: string;
  description?: string;
  monthlyPrice: number;
  annualPrice?: number;
  maxBranches: number;
  maxPosTerminals?: number;
  maxUsers: number;
  maxItems?: number;
  maxStorageMB?: number;
  allowedModulesJson?: string;
  features?: string[];
  isActive?: boolean;
}

export enum BusinessType {
  Retail = 1,
  Restaurant = 2,
  Service = 3,
  Other = 4
}

export interface BusinessTypeFeature {
  businessType: number;
  businessTypeName?: string;
  featureKey: number;
  isEnabledByDefault: boolean;
}

export interface CreateBusinessTypeFeatureCommand {
  businessType: number;
  featureKey: number;
}

export interface UpdateBusinessTypeFeaturesCommand {
  businessType: number;
  featureKeys: number[];
}

export interface TenantFeatureFlag {
  tenantId: string;
  featureKey: number;
  isEnabled: boolean;
  configJson?: string;
  enabledByUserId?: string;
}

export interface UpsertTenantFeatureFlagCommand {
  tenantId: string;
  featureKey: number;
  isEnabled: boolean;
  configJson?: string;
  enabledByUserId?: string;
}

export interface PagedResponse<T> {
  data: T[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalRecords: number;
}

export interface BaseResponse<T> {
  id?: string;
  succeeded: boolean;
  message?: string;
  errors?: string[];
  data: T;
}
