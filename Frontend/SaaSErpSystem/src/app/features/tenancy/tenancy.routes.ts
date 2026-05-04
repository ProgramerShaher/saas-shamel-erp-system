import { Routes } from '@angular/router';

export const TENANCY_ROUTES: Routes = [
  {
    path: 'tenants',
    loadComponent: () => import('./pages/tenant-list/tenant-list.component').then(m => m.TenantListComponent),
    data: { title: 'المنشآت' }
  },
  {
    path: 'tenants/new',
    loadComponent: () => import('./pages/tenant-form/tenant-form.component').then(m => m.TenantFormComponent),
    data: { title: 'إضافة منشأة' }
  },
  {
    path: 'tenants/:id/edit',
    loadComponent: () => import('./pages/tenant-form/tenant-form.component').then(m => m.TenantFormComponent),
    data: { title: 'تعديل منشأة' }
  },
  {
    path: 'plans',
    loadComponent: () => import('./pages/plan-list/plan-list.component').then(m => m.PlanListComponent),
    data: { title: 'خطط الاشتراك' }
  },
  {
    path: 'features',
    loadComponent: () => import('./pages/feature-management/feature-management.component').then(m => m.FeatureManagementComponent),
    data: { title: 'إدارة المميزات' }
  },
  { path: '', redirectTo: 'tenants', pathMatch: 'full' }
];


