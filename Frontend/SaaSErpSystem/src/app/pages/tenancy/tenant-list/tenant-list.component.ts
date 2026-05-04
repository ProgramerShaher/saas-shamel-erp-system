import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TenancyService } from '../../../features/tenancy/services/tenancy.service';
import { NotificationService } from '../../../core/services/notification.service';
import { Tenant } from '../../../features/tenancy/models/tenancy.model';


@Component({
  selector: 'app-tenant-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.css']
})
export class TenantListComponent implements OnInit {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);

  readonly tenants = signal<Tenant[]>([]);

  ngOnInit() {
    this.loadTenants();
  }

  loadTenants() {
    this.tenancyService.getTenants().subscribe({
      next: (data: Tenant[]) => this.tenants.set(data),
      error: (err: any) => this.notification.error('فشل في تحميل قائمة المنشآت')
    });
  }

  deleteTenant(id: string) {
    if (confirm('هل أنت متأكد من حذف هذه المنشأة؟')) {
      this.tenancyService.deleteTenant(id).subscribe({
        next: () => {
          this.notification.success('تم حذف المنشأة بنجاح');
          this.loadTenants();
        },
        error: (err: any) => this.notification.error('حدث خطأ أثناء محاولة الحذف')
      });
    }
  }

  getBusinessTypeName(type: number): string {
    switch (type) {
      case 1: return 'تجارة تجزئة';
      case 2: return 'مطعم / كافيه';
      case 3: return 'خدمات';
      default: return 'أخرى';
    }
  }
}
