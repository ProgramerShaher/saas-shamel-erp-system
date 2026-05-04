import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UIStore } from '../../store/ui.store';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  readonly ui = inject(UIStore);

  readonly menuItems = [
    { label: 'لوحة التحكم', icon: 'pi pi-home', route: '/dashboard' },
    { label: 'المنشآت', icon: 'pi pi-building', route: '/tenancy/tenants' },
    { label: 'خطط الاشتراك', icon: 'pi pi-list', route: '/tenancy/plans' },
    { label: 'إدارة المميزات', icon: 'pi pi-bolt', route: '/tenancy/features' },
    { label: 'المخزون', icon: 'pi pi-warehouse', route: '/inventory' },
    { label: 'نقطة البيع', icon: 'pi pi-desktop', route: '/pos' },
    { label: 'المبيعات', icon: 'pi pi-shopping-cart', route: '/sales' },
    { label: 'المشتريات', icon: 'pi pi-truck', route: '/purchasing' },
    { label: 'المالية', icon: 'pi pi-wallet', route: '/finance' },
    { label: 'الإعدادات', icon: 'pi pi-cog', route: '/settings' },
  ];
}
