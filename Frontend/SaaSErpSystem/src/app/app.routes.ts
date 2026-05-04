import { Routes } from '@angular/router';

export const routes: Routes = [
  // 1. التوجيه التلقائي عند فتح الموقع
  { path: '', redirectTo: 'onboarding/setup', pathMatch: 'full' },

  // 2. مسار الـ Wizard (مستقل بملء الشاشة)
  {
    path: 'onboarding/setup',
    loadComponent: () => import('./features/tenancy/pages/tenant-wizard/tenant-wizard.component').then(m => m.TenantWizardComponent),
    data: { title: 'تهيئة المنشأة' }
  },

  // 3. مسارات النظام (داخل الـ Layout)
  {
    path: '',
    loadComponent: () => import('./core/layout/main-layout/main-layout.component').then(m => m.MainLayoutComponent),
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'لوحة التحكم' }
      },
      {
        path: 'tenancy',
        loadChildren: () => import('./features/tenancy/tenancy.routes').then(m => m.TENANCY_ROUTES)
      },
      {
        path: 'inventory',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'المخزون' }
      },
      {
        path: 'pos',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'نقطة البيع' }
      },
      {
        path: 'sales',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'المبيعات' }
      },
      {
        path: 'purchasing',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'المشتريات' }
      },
      {
        path: 'finance',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'المالية' }
      },
      {
        path: 'settings',
        loadComponent: () => import('./shared/components/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { title: 'الإعدادات' }
      }
    ]
  }
];
