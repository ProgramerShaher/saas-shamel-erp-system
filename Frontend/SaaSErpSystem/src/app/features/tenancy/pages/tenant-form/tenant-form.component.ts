import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TenancyService } from '../../services/tenancy.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { CreateTenantCommand, UpdateTenantCommand, SubscriptionPlan } from '../../models/tenancy.model';

@Component({
  selector: 'app-tenant-form',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './tenant-form.component.html',
  styleUrls: ['./tenant-form.component.css']
})
export class TenantFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly isEditMode = signal(false);
  readonly isSaving = signal(false);
  readonly plans = signal<SubscriptionPlan[]>([]);
  showPassword = false;

  readonly businessTypes = [
    { id: 1, name: 'مول تجاري' },
    { id: 2, name: 'سوبر ماركت' },
    { id: 3, name: 'هايبر ماركت' },
    { id: 4, name: 'صيدلية' },
    { id: 5, name: 'مكتبة' },
    { id: 6, name: 'بقالة' },
    { id: 7, name: 'مطعم' },
    { id: 8, name: 'محل تجاري' },
    { id: 9, name: 'كفتيريا' }
  ];

  form: FormGroup = this.fb.group({
    id: [null],
    name: ['', [Validators.required, Validators.minLength(3)]],
    slug: ['', [Validators.required, Validators.pattern(/^[a-z0-9-]+$/)]],
    businessType: [1, Validators.required],
    subscriptionPlanId: ['', Validators.required],
    isActive: [true],
    logoUrl: [''],
    primaryColor: ['#1E3A5F'],
    adminEmail: ['', [Validators.required, Validators.email]],
    adminPassword: ['', [Validators.required, Validators.minLength(6)]]
  });

  ngOnInit() {
    this.loadPlans();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode.set(true);
      this.loadTenant(id);
      // في وضع التعديل، لا نطلب بيانات الدخول
      this.form.get('adminEmail')?.clearValidators();
      this.form.get('adminEmail')?.updateValueAndValidity();
      this.form.get('adminPassword')?.clearValidators();
      this.form.get('adminPassword')?.updateValueAndValidity();
    }
  }

  private loadPlans() {
    this.tenancyService.getSubscriptionPlans().subscribe({
      next: (data) => {
        if (data && data.length > 0) {
          this.plans.set(data);
        } else {
          // بيانات تجريبية في حال السيرفر متوقف
          this.plans.set([
            { id: '1', planName: 'الخطة الأساسية', monthlyPrice: 49, maxUsers: 5, maxBranches: 1 },
            { id: '2', planName: 'الخطة الاحترافية', monthlyPrice: 99, maxUsers: 20, maxBranches: 5 },
            { id: '3', planName: 'خطة الشركات', monthlyPrice: 199, maxUsers: 50, maxBranches: 15 }
          ]);
        }
      },
      error: () => {
        this.plans.set([
          { id: '1', planName: 'الخطة الأساسية', monthlyPrice: 49, maxUsers: 5, maxBranches: 1 },
          { id: '2', planName: 'الخطة الاحترافية', monthlyPrice: 99, maxUsers: 20, maxBranches: 5 },
          { id: '3', planName: 'خطة الشركات', monthlyPrice: 199, maxUsers: 50, maxBranches: 15 }
        ]);
      }
    });
  }

  loadTenant(id: string) {
    this.tenancyService.getTenantById(id).subscribe({
      next: (tenant: any) => {
        this.form.patchValue({
          ...tenant,
          businessType: Number(tenant.businessType)
        });
      },
      error: () => this.notification.error('خطأ في جلب بيانات المنشأة')
    });
  }

  save() {
    if (this.form.invalid) {
      this.notification.warn('يرجى التأكد من إكمال جميع الحقول المطلوبة');
      return;
    }

    this.isSaving.set(true);
    const val = this.form.value;

    if (this.isEditMode()) {
      const command: UpdateTenantCommand = {
        id: val.id,
        name: val.name,
        isActive: val.isActive,
        businessType: Number(val.businessType),
        subscriptionPlanId: val.subscriptionPlanId,
        logoUrl: val.logoUrl,
        primaryColor: val.primaryColor
      };
      this.tenancyService.updateTenant(command).subscribe({
        next: () => {
          this.notification.success('تم تحديث البيانات بنجاح');
          this.router.navigate(['/tenancy/tenants']);
        },
        error: (err: any) => {
          this.notification.error(err.error?.Message || 'خطأ في التحديث');
          this.isSaving.set(false);
        }
      });
    } else {
      const command: CreateTenantCommand = {
        name: val.name,
        slug: val.slug,
        businessType: Number(val.businessType),
        subscriptionPlanId: val.subscriptionPlanId,
        primaryColor: val.primaryColor,
        logoUrl: val.logoUrl
      };
      this.tenancyService.createTenant(command).subscribe({
        next: () => {
          this.notification.success('تم إنشاء المنشأة بنجاح');
          this.router.navigate(['/tenancy/tenants']);
        },
        error: (err: any) => {
          this.notification.error(err.error?.Message || 'خطأ في إنشاء المنشأة');
          this.isSaving.set(false);
        }
      });
    }
  }
}
