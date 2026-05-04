import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TenancyService } from '../../services/tenancy.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { 
  BusinessType, 
  BusinessTypeFeature, 
  Tenant, 
  TenantFeatureFlag,
  UpsertTenantFeatureFlagCommand,
  CreateBusinessTypeFeatureCommand
} from '../../models/tenancy.model';
import { ModalComponent } from '../../../../shared/components/modal/modal.component';

@Component({
  selector: 'app-feature-management',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ModalComponent],
  templateUrl: './feature-management.component.html',
  styleUrls: ['./feature-management.component.css']
})
export class FeatureManagementComponent implements OnInit {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);
  private readonly fb = inject(FormBuilder);

  readonly businessTypes = [
    { id: 1, name: 'مول تجاري (Retail)' },
    { id: 2, name: 'سوبر ماركت (Restaurant/Food)' }, 
    { id: 3, name: 'هايبر ماركت (Service)' },
    { id: 4, name: 'أخرى (Other)' }
  ];

  readonly selectedBusinessType = signal<number>(1);
  readonly businessTypeFeatures = signal<BusinessTypeFeature[]>([]);
  readonly tenants = signal<Tenant[]>([]);
  readonly selectedTenant = signal<Tenant | null>(null);
  readonly tenantFeatureFlags = signal<TenantFeatureFlag[]>([]);
  
  readonly isLoading = signal(false);
  readonly showTenantModal = signal(false);
  readonly isSaving = signal(false);

  readonly availableFeatures = [
    { id: 10, name: 'إدارة المخزون (Inventory)' },
    { id: 20, name: 'نقطة البيع (POS)' },
    { id: 30, name: 'طلبات الشراء (Purchasing)' },
    { id: 40, name: 'مراكز التكلفة (Cost Centers)' },
    { id: 70, name: 'وحدة الصيدلية (Pharmacy)' },
    { id: 71, name: 'وحدة المطاعم (Restaurant)' }
  ];

  featureForm: FormGroup = this.fb.group({
    featureKey: ['', Validators.required],
    isEnabledByDefault: [true]
  });

  tenantFeatureForm: FormGroup = this.fb.group({
    featureKey: ['', Validators.required],
    isEnabled: [true],
    configJson: ['']
  });

  ngOnInit() {
    this.loadBusinessTypeFeatures(this.selectedBusinessType());
    this.loadTenants();
  }

  loadBusinessTypeFeatures(type: number) {
    this.isLoading.set(true);
    this.tenancyService.getBusinessTypeFeatures(type).subscribe({
      next: (features) => {
        this.businessTypeFeatures.set(features);
        this.isLoading.set(false);
      },
      error: () => {
        this.notification.error('فشل في تحميل مميزات نوع النشاط');
        this.isLoading.set(false);
      }
    });
  }

  loadTenants() {
    this.tenancyService.getTenants().subscribe({
      next: (tenants) => this.tenants.set(tenants),
      error: () => this.notification.error('فشل في تحميل قائمة المنشآت')
    });
  }

  onBusinessTypeChange(event: any) {
    const type = Number(event);
    this.selectedBusinessType.set(type);
    this.loadBusinessTypeFeatures(type);
  }

  addBusinessTypeFeature() {
    if (this.featureForm.invalid) return;
    
    const command: CreateBusinessTypeFeatureCommand = {
      businessType: this.selectedBusinessType(),
      featureKey: Number(this.featureForm.value.featureKey)
    };

    this.isSaving.set(true);
    this.tenancyService.createBusinessTypeFeature(command).subscribe({
      next: () => {
        this.notification.success('تم إضافة الميزة بنجاح');
        this.loadBusinessTypeFeatures(this.selectedBusinessType());
        this.featureForm.reset({ isEnabledByDefault: true });
        this.isSaving.set(false);
      },
      error: (err) => {
        this.notification.error(err.error?.message || 'فشل في إضافة الميزة');
        this.isSaving.set(false);
      }
    });
  }

  manageTenantFeatures(tenant: Tenant) {
    this.selectedTenant.set(tenant);
    this.isLoading.set(true);
    this.tenancyService.getTenantFeatureFlags(tenant.id).subscribe({
      next: (flags) => {
        this.tenantFeatureFlags.set(flags);
        this.showTenantModal.set(true);
        this.isLoading.set(false);
      },
      error: () => {
        this.notification.error('فشل في تحميل خصائص المنشأة');
        this.isLoading.set(false);
      }
    });
  }

  upsertTenantFeature() {
    if (this.tenantFeatureForm.invalid || !this.selectedTenant()) return;

    const command: UpsertTenantFeatureFlagCommand = {
      tenantId: this.selectedTenant()!.id,
      featureKey: Number(this.tenantFeatureForm.value.featureKey),
      isEnabled: this.tenantFeatureForm.value.isEnabled,
      configJson: this.tenantFeatureForm.value.configJson
    };

    this.isSaving.set(true);
    this.tenancyService.upsertTenantFeatureFlag(command).subscribe({
      next: () => {
        this.notification.success('تم تحديث خاصية المنشأة');
        this.manageTenantFeatures(this.selectedTenant()!);
        this.tenantFeatureForm.reset({ isEnabled: true });
        this.isSaving.set(false);
      },
      error: (err) => {
        this.notification.error(err.error?.message || 'فشل في التحديث');
        this.isSaving.set(false);
      }
    });
  }

  getFeatureName(key: number): string {
    const feature = this.availableFeatures.find(f => f.id === key);
    return feature ? feature.name : `ميزة #${key}`;
  }
}
