import { Component, inject, signal, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Tenant, CreateTenantCommand, UpdateTenantCommand, SubscriptionPlan } from '../../models/tenancy.model';
import { TenancyService } from '../../services/tenancy.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { ModalComponent } from '../../../../shared/components/modal/modal.component';

@Component({
  selector: 'app-tenant-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ModalComponent],
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.css']
})
export class TenantListComponent implements OnInit {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);
  private readonly fb = inject(FormBuilder);
  private readonly platformId = inject(PLATFORM_ID);

  readonly tenants = signal<Tenant[]>([]);
  readonly isLoading = signal(false);
  readonly showModal = signal(false);
  readonly isSaving = signal(false);
  readonly editingTenant = signal<Tenant | null>(null);
  readonly plans = signal<SubscriptionPlan[]>([]);
  readonly currentStep = signal(1);
  readonly logoPreview = signal<string | null>(null);
  showPassword = false;

  form: FormGroup = this.fb.group({
    id: [null],
    name: ['', [Validators.required, Validators.minLength(3)]],
    slug: ['', [Validators.required, Validators.pattern(/^[a-z0-9-]+$/)]],
    businessType: [1, Validators.required],
    subscriptionPlanId: ['', Validators.required],
    isActive: [true],
    primaryColor: ['#1E3A5F'],
    logoUrl: ['']
  });

  readonly activeTenants = () => this.tenants().filter(t => t.isActive).length;

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadTenants();
      this.loadPlans();
    }
  }

  loadPlans() {
    this.tenancyService.getSubscriptionPlans().subscribe({
      next: (data) => {
        this.plans.set(data || []);
      },
      error: () => {
        this.notification.error('فشل في تحميل باقات الاشتراك');
      }
    });
  }

  loadTenants() {
    this.isLoading.set(true);
    this.tenancyService.getTenants().subscribe({
      next: (data: Tenant[]) => {
        this.tenants.set(data || []);
        this.isLoading.set(false);
      },
      error: (err: any) => {
        console.error('Tenants load error:', err);
        this.notification.error('فشل في تحميل قائمة المنشآت');
        this.isLoading.set(false);
      },
      complete: () => {
        this.isLoading.set(false);
      }
    });
  }

  openCreate() {
    this.editingTenant.set(null);
    this.currentStep.set(1);
    this.logoPreview.set(null);
    this.form.reset({ businessType: 1, isActive: true, primaryColor: '#1E3A5F' });
    this.form.updateValueAndValidity();
    this.showModal.set(true);
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result as string;
        this.logoPreview.set(base64);
        this.form.patchValue({ logoUrl: base64 });
      };
      reader.readAsDataURL(file);
    }
  }

  triggerFileInput(fileInput: HTMLInputElement) {
    fileInput.click();
  }

  nextStep() {
    if (this.currentStep() < 2) {
      this.currentStep.update(s => s + 1);
    }
  }

  prevStep() {
    if (this.currentStep() > 1) {
      this.currentStep.update(s => s - 1);
    }
  }

  openEdit(tenant: Tenant) {
    this.editingTenant.set(tenant);
    this.logoPreview.set(tenant.logoUrl || null);
    this.form.patchValue(tenant);
    this.form.updateValueAndValidity();
    this.showModal.set(true);
  }

  closeModal() {
    this.showModal.set(false);
    this.editingTenant.set(null);
    this.form.reset();
  }

  save() {
    if (this.form.invalid) return;
    this.isSaving.set(true);
    const val = this.form.value;

    if (this.editingTenant()) {
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
          this.notification.success('تم تحديث بيانات المنشأة بنجاح');
          this.isSaving.set(false);
          this.closeModal();
          this.loadTenants();
        },
        error: (err: any) => {
          this.notification.error(err.error?.Message || 'خطأ أثناء التحديث');
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
          this.isSaving.set(false);
          this.closeModal();
          this.loadTenants();
        },
        error: (err: any) => {
          this.notification.error(err.error?.Message || 'خطأ أثناء إنشاء المنشأة');
          this.isSaving.set(false);
        }
      });
    }
  }

  deleteTenant(id: string) {
    if (!confirm('هل أنت متأكد من حذف هذه المنشأة؟ لا يمكن التراجع عن هذه العملية.')) return;
    this.tenancyService.deleteTenant(id).subscribe({
      next: () => {
        this.notification.success('تم حذف المنشأة بنجاح');
        this.loadTenants();
      },
      error: (err: any) => this.notification.error('حدث خطأ أثناء محاولة الحذف')
    });
  }

  getBusinessTypeName(type: number): string {
    const types: Record<number, string> = {
      1: 'مول تجاري', 2: 'سوبر ماركت', 3: 'هايبر ماركت',
      4: 'صيدلية', 5: 'مكتبة', 6: 'بقالة',
      7: 'مطعم', 8: 'محل تجاري', 9: 'كفتيريا'
    };
    return types[type] || 'أخرى';
  }

  getImageUrl(url: string | null | undefined): string {
    if (!url) return '';
    if (url.startsWith('http') || url.startsWith('data:image')) {
      return url;
    }
    // إضافة رابط الـ API للروابط النسبية
    return `http://localhost:5064${url}`;
  }
}
