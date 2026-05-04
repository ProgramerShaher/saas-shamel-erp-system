import { signal, computed, inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class UIStore {
  private readonly platformId = inject(PLATFORM_ID);
  private readonly document = inject(DOCUMENT);
  
  readonly sidebarCollapsed = signal<boolean>(false);
  readonly currentLang = signal<'ar' | 'en'>('ar');

  constructor() {
    this.setLanguage(this.currentLang());
  }

  toggleSidebar() {
    this.sidebarCollapsed.update(v => !v);
  }

  setLanguage(lang: 'ar' | 'en') {
    this.currentLang.set(lang);
    
    if (isPlatformBrowser(this.platformId)) {
      this.document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';
      this.document.documentElement.lang = lang;
    }
  }
}
