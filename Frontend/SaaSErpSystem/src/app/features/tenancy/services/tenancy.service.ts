import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of, catchError } from 'rxjs';
import { 
  Tenant, 
  CreateTenantCommand, 
  UpdateTenantCommand, 
  SubscriptionPlan, 
  PagedResponse,
  BaseResponse,
  BusinessTypeFeature,
  CreateBusinessTypeFeatureCommand,
  UpdateBusinessTypeFeaturesCommand,
  TenantFeatureFlag,
  UpsertTenantFeatureFlagCommand
} from '../models/tenancy.model';

@Injectable({
  providedIn: 'root'
})
export class TenancyService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5064/api/v1';

  // --- Tenants ---
  getTenants(pageNumber: number = 1, pageSize: number = 50): Observable<Tenant[]> {
    return this.http.get<PagedResponse<Tenant>>(`${this.baseUrl}/Tenants?pageNumber=${pageNumber}&pageSize=${pageSize}`)
      .pipe(
        map(response => response?.data || []),
        catchError(err => {
          console.error('Error fetching tenants:', err);
          return of([]);
        })
      );
  }

  getTenantById(id: string): Observable<Tenant> {
    return this.http.get<Tenant>(`${this.baseUrl}/Tenants/${id}`);
  }

  createTenant(command: CreateTenantCommand): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/Tenants`, command);
  }

  updateTenant(command: UpdateTenantCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/Tenants/${command.id}`, command);
  }

  deleteTenant(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Tenants/${id}`);
  }

  // --- Subscription Plans ---
  getSubscriptionPlans(pageNumber: number = 1, pageSize: number = 50): Observable<SubscriptionPlan[]> {
    return this.http.get<PagedResponse<SubscriptionPlan>>(`${this.baseUrl}/SubscriptionPlans?pageNumber=${pageNumber}&pageSize=${pageSize}`)
      .pipe(
        map(response => response?.data || []),
        catchError(err => {
          console.error('Error fetching plans:', err);
          return of([]);
        })
      );
  }

  createSubscriptionPlan(plan: Partial<SubscriptionPlan>): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/SubscriptionPlans`, plan);
  }

  // --- Business Type Features ---
  getBusinessTypeFeatures(businessType?: number): Observable<BusinessTypeFeature[]> {
    const url = businessType !== undefined 
      ? `${this.baseUrl}/BusinessTypeFeatures/${businessType}` 
      : `${this.baseUrl}/BusinessTypeFeatures`;
    
    return this.http.get<BaseResponse<BusinessTypeFeature[]>>(url).pipe(
      map(response => response.data || []),
      catchError(err => {
        console.error('Error fetching business type features:', err);
        return of([]);
      })
    );
  }

  createBusinessTypeFeature(command: CreateBusinessTypeFeatureCommand): Observable<BaseResponse<string>> {
    return this.http.post<BaseResponse<string>>(`${this.baseUrl}/BusinessTypeFeatures`, command);
  }

  updateBusinessTypeFeatures(command: UpdateBusinessTypeFeaturesCommand): Observable<BaseResponse<void>> {
    return this.http.put<BaseResponse<void>>(`${this.baseUrl}/BusinessTypeFeatures`, command);
  }

  deleteBusinessTypeFeature(): Observable<BaseResponse<void>> {
    return this.http.delete<BaseResponse<void>>(`${this.baseUrl}/BusinessTypeFeatures`);
  }

  // --- Tenant Feature Flags ---
  getTenantFeatureFlags(tenantId: string): Observable<TenantFeatureFlag[]> {
    return this.http.get<BaseResponse<TenantFeatureFlag[]>>(`${this.baseUrl}/TenantFeatureFlags/${tenantId}`).pipe(
      map(response => response.data || []),
      catchError(err => {
        console.error('Error fetching tenant feature flags:', err);
        return of([]);
      })
    );
  }

  upsertTenantFeatureFlag(command: UpsertTenantFeatureFlagCommand): Observable<BaseResponse<void>> {
    return this.http.post<BaseResponse<void>>(`${this.baseUrl}/TenantFeatureFlags/upsert`, command);
  }
}
