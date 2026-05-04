import { Injectable, signal, inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

export type NotificationType = 'success' | 'error' | 'info' | 'warning';

export interface Notification {
  id: number;
  message: string;
  type: NotificationType;
  title?: string;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  readonly notifications = signal<Notification[]>([]);
  private readonly platformId = inject(PLATFORM_ID);
  private counter = 0;

  show(message: string, type: NotificationType = 'info', title?: string) {
    const id = this.counter++;
    const newNotification: Notification = { id, message, type, title };

    this.notifications.update(list => [...list, newNotification]);

    // Auto-remove after 5 seconds
    if (isPlatformBrowser(this.platformId)) {
      setTimeout(() => {
        this.remove(id);
      }, 5000);
    }
  }

  success(message: string, title: string = 'نجاح') {
    this.show(message, 'success', title);
  }

  error(message: string, title: string = 'خطأ') {
    this.show(message, 'error', title);
  }

  warn(message: string, title: string = 'تنبيه') {
    this.show(message, 'warning', title);
  }

  info(message: string, title: string = 'معلومة') {
    this.show(message, 'info', title);
  }

  remove(id: number) {
    this.notifications.update(list => list.filter(n => n.id !== id));
  }
}
