import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TenancyService } from '../../services/tenancy.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { CreateTenantCommand, SubscriptionPlan } from '../../models/tenancy.model';

@Component({
  selector: 'app-tenant-wizard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './tenant-wizard.component.html',
  styleUrls: ['./tenant-wizard.component.css']
})
export class TenantWizardComponent {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);

  readonly currentStep = signal(1);
  readonly isSaving = signal(false);
  readonly logoPreview = signal<string | null>(null);
  readonly plans = signal<SubscriptionPlan[]>([]);

  readonly businessTypes = [
    { id: 1, name: 'مول تجاري', icon: 'pi-shopping-cart' },
    { id: 2, name: 'سوبر ماركت', icon: 'pi-shopping-bag' },
    { id: 3, name: 'هايبر ماركت', icon: 'pi-building' },
    { id: 4, name: 'صيدلية', icon: 'pi-heart-fill' },
    { id: 5, name: 'مكتبة', icon: 'pi-book' },
    { id: 6, name: 'بقالة', icon: 'pi-shop' },
    { id: 7, name: 'مطعم', icon: 'pi-map' },
    { id: 8, name: 'محل تجاري', icon: 'pi-tag' },
    { id: 9, name: 'كفتيريا', icon: 'pi-coffee' }
  ];

  form: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    slug: ['', [Validators.required, Validators.pattern(/^[a-z0-9-]+$/)]],
    businessType: [1, Validators.required],
    subscriptionPlanId: ['', Validators.required],
    primaryColor: ['#1E3A5F'],
    logoUrl: ['']
  });

  constructor() {
    this.loadPlans();
  }

  private loadPlans() {
    this.tenancyService.getSubscriptionPlans().subscribe({
      next: (data) => this.plans.set(data || []),
      error: () => this.notification.error('فشل تحميل باقات الاشتراك')
    });
  }

  nextStep() {
    if (this.currentStep() === 1 && (this.form.get('name')?.invalid || this.form.get('slug')?.invalid)) {
      this.notification.warn('يرجى التأكد من اسم المنشأة والنطاق');
      return;
    }
    if (this.currentStep() < 4) {
      this.currentStep.update(s => s + 1);
    }
  }

  prevStep() {
    if (this.currentStep() > 1) {
      this.currentStep.update(s => s - 1);
    }
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

  completeSetup() {
    if (this.form.invalid) {
      this.notification.error('يرجى إكمال جميع البيانات المطلوبة');
      return;
    }

    this.isSaving.set(true);
    const val = this.form.value;
    const command: CreateTenantCommand = {
      ...val,
      businessType: Number(val.businessType)
    };

    this.tenancyService.createTenant(command).subscribe({
      next: (response) => {
        console.log('Tenant Created Successfully:', response);
        this.notification.success('تم تهيئة حسابك بنجاح!');
        // التوجيه الفوري لضمان عدم حدوث تعليق
        this.router.navigateByUrl('/dashboard');
      },
      error: (err) => {
        console.error('Error creating tenant:', err);
        this.notification.error(err.error?.Message || 'حدث خطأ أثناء الإنشاء');
        this.isSaving.set(false);
      }
    });
  }
}
