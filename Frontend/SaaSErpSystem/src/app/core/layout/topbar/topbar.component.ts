import { Component, inject, signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs';
import { UIStore } from '../../store/ui.store';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css']
})
export class TopbarComponent {
  readonly ui = inject(UIStore);
  private readonly router = inject(Router);
  private readonly activatedRoute = inject(ActivatedRoute);

  readonly pageTitle = signal<string>('لوحة التحكم');

  constructor() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => {
        let route = this.activatedRoute.firstChild;
        while (route?.firstChild) {
          route = route.firstChild;
        }
        return route?.snapshot.data['title'] || 'لوحة التحكم';
      })
    ).subscribe(title => this.pageTitle.set(title));
  }

  toggleLanguage() {
    const newLang = this.ui.currentLang() === 'ar' ? 'en' : 'ar';
    this.ui.setLanguage(newLang);
  }
}
