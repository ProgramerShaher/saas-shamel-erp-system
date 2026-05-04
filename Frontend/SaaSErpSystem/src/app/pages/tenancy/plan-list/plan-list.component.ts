import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TenancyService } from '../../../features/tenancy/services/tenancy.service';
import { NotificationService } from '../../../core/services/notification.service';
import { SubscriptionPlan } from '../../../features/tenancy/models/tenancy.model';



@Component({
  selector: 'app-plan-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit {
  private readonly tenancyService = inject(TenancyService);
  private readonly notification = inject(NotificationService);

  readonly plans = signal<SubscriptionPlan[]>([]);

  ngOnInit() {
    this.loadPlans();
  }

  loadPlans() {
    this.tenancyService.getSubscriptionPlans().subscribe({
      next: (data) => this.plans.set(data),
      error: (err: any) => this.notification.error('فشل في تحميل خطط الاشتراك')
    });
  }
}
