import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TenancyService } from '../../services/tenancy.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { SubscriptionPlan } from '../../models/tenancy.model';
import { ModalComponent } from '../../../../shared/components/modal/modal.component';

@Component({
  selector: 'app-plan-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ModalComponent],
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);
  private readonly fb = inject(FormBuilder);

  readonly plans = signal<SubscriptionPlan[]>([]);
  readonly isLoading = signal(false);
  readonly showModal = signal(false);
  readonly isSaving = signal(false);
  readonly editingPlan = signal<SubscriptionPlan | null>(null);

  form: FormGroup = this.fb.group({
    id: [null],
    planName: ['', Validators.required],
    description: [''],
    monthlyPrice: [0, [Validators.required, Validators.min(0)]],
    annualPrice: [0],
    maxBranches: [1, [Validators.required, Validators.min(1)]],
    maxPosTerminals: [1, [Validators.required, Validators.min(1)]],
    maxUsers: [5, [Validators.required, Validators.min(1)]],
    maxItems: [1000, [Validators.required, Validators.min(1)]],
    allowedModulesJson: ['[]'],
    isActive: [true]
  });

  ngOnInit() {
    this.loadPlans();
  }

  loadPlans() {
    this.isLoading.set(true);
    this.tenancyService.getSubscriptionPlans().subscribe({
      next: (data: SubscriptionPlan[]) => {
        this.plans.set(data);
        this.isLoading.set(false);
      },
      error: (err: any) => {
        this.notification.error('فشل في تحميل خطط الاشتراك');
        this.isLoading.set(false);
      }
    });
  }

  openCreate() {
    this.editingPlan.set(null);
    this.form.reset({ monthlyPrice: 0, annualPrice: 0, maxBranches: 1, maxPosTerminals: 1, maxUsers: 5, maxItems: 1000, isActive: true, allowedModulesJson: '[]' });
    this.showModal.set(true);
  }

  openEdit(plan: SubscriptionPlan) {
    this.editingPlan.set(plan);
    this.form.patchValue(plan);
    this.showModal.set(true);
  }

  closeModal() {
    this.showModal.set(false);
    this.editingPlan.set(null);
    this.form.reset();
  }

  save() {
    if (this.form.invalid) return;
    this.isSaving.set(true);
    const val = this.form.value;

    const plan: Partial<SubscriptionPlan> = {
      planName: val.planName,
      description: val.description,
      monthlyPrice: Number(val.monthlyPrice),
      annualPrice: Number(val.annualPrice),
      maxBranches: Number(val.maxBranches),
      maxPosTerminals: Number(val.maxPosTerminals),
      maxUsers: Number(val.maxUsers),
      maxItems: Number(val.maxItems),
      allowedModulesJson: val.allowedModulesJson || '[]',
      isActive: val.isActive
    };

    this.tenancyService.createSubscriptionPlan(plan).subscribe({
      next: () => {
        this.notification.success(this.editingPlan() ? 'تم تحديث الخطة بنجاح' : 'تم إنشاء الخطة بنجاح');
        this.isSaving.set(false);
        this.closeModal();
        this.loadPlans();
      },
      error: (err: any) => {
        this.notification.error(err.error?.Message || 'خطأ أثناء حفظ الخطة');
        this.isSaving.set(false);
      }
    });
  }
}
