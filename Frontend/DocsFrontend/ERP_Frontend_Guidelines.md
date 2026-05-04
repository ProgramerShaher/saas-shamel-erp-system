# ⚡ ERP SYSTEM — دليل تطوير الواجهة الأمامية

> **هذا الملف هو المرجع الرسمي للمشروع.**
> كل من يعمل على المشروع — سواء كان مطوراً بشرياً أو ذكاءً اصطناعياً — يجب أن يقرأ هذا الملف أولاً قبل كتابة أي سطر كود.

---

## 📋 جدول المحتويات

1. [نظرة عامة على المشروع](#1-نظرة-عامة-على-المشروع)
2. [المكدس التقني](#2-المكدس-التقني)
3. [القواعد الصارمة — لا استثناء](#3-القواعد-الصارمة--لا-استثناء)
4. [هيكلية المجلدات](#4-هيكلية-المجلدات)
5. [قواعد المكوّنات](#5-قواعد-المكوّنات)
6. [نظام التصميم](#6-نظام-التصميم)
7. [بنية الصفحات](#7-بنية-الصفحات)
8. [CSS Classes العالمية](#8-css-classes-العالمية)
9. [نظام Feature Flags](#9-نظام-feature-flags)
10. [الفرز والتصفية والصفحات المتعددة](#10-الفرز-والتصفية-والصفحات-المتعددة)
11. [HTTP والـ API Services](#11-http-والـ-api-services)
12. [قواعد التسمية](#12-قواعد-التسمية)
13. [قائمة الوحدات وصفحاتها](#13-قائمة-الوحدات-وصفحاتها)
14. [افعل ولا تفعل](#14-افعل-ولا-تفعل)
15. [إعدادات البيئة](#15-إعدادات-البيئة)
16. [نظام الإشعارات (Toast Notifications)](#16-نظام-الإشعارات-toast-notifications)

---

## 1. نظرة عامة على المشروع

نظام ERP سحابي يدعم أنواع أعمال متعددة (مول، صيدلية، بقالة، إلكترونيات...).
المشروع يُطوَّر بالتعاون بين مطور بشري وذكاء اصطناعي، لذلك **الاتساق والوضوح** هما الأولوية القصوى.

النظام يتكيّف ديناميكياً حسب نوع العمل عبر **Feature Flags** — أي أن بعض الأقسام تظهر أو تختفي بناءً على إعدادات المنشأة.

---

## 2. المكدس التقني

| التقنية | الإصدار / الغرض |
|---------|----------------|
| Angular | v21 — Standalone Components فقط |
| PrimeNG | v17+ — مكتبة UI الرئيسية |
| Tailwind CSS | v3 — التصميم والألوان والتخطيط |
| NgRx Signals | إدارة الـ State |
| RxJS | HTTP والـ Reactive Streams |
| TypeScript | v5+ — strict mode مفعّل |
| PrimeIcons | الأيقونات الرئيسية |

---

## 3. القواعد الصارمة — لا استثناء

> ⛔ هذه القواعد غير قابلة للنقاش. أي كود لا يلتزم بها يُرفض.

```
✅  كل component يجب أن يكون standalone: true
✅  كل component له 3 ملفات فقط: .ts  +  .html  +  .css
✅  في ملف .ts يجب كتابة templateUrl مع مسار ملف .html الخارجي
✅  في ملف .ts يجب كتابة styleUrl مع مسار ملف .css الخارجي
✅  Tailwind CSS للتصميم — لا تكتب CSS مخصصاً إلا للضرورة القصوى
✅  لا تستخدم NgModules — Standalone فقط
✅  لا تستخدم any كنوع بيانات — استخدم TypeScript types دائماً
```

---

## 4. هيكلية المجلدات

> 📌 هذه الهيكلية ثابتة ولا تتغير. كل ملف جديد يُوضع في مكانه الصحيح.

```
src/
├── app/
│   ├── core/                      ← Services/Guards/Interceptors العالمية
│   │   ├── guards/
│   │   │   ├── auth.guard.ts
│   │   │   ├── feature.guard.ts
│   │   │   └── permission.guard.ts
│   │   ├── interceptors/
│   │   │   ├── auth.interceptor.ts
│   │   │   ├── tenant.interceptor.ts
│   │   │   └── error.interceptor.ts
│   │   ├── services/
│   │   │   ├── auth.service.ts
│   │   │   ├── feature.service.ts
│   │   │   └── tenant.service.ts
│   │   └── models/
│   │       ├── auth.model.ts
│   │       └── tenant.model.ts
│   │
│   ├── shared/                    ← مكونات مشتركة بين جميع الوحدات
│   │   ├── components/
│   │   │   ├── page-header/
│   │   │   │   ├── page-header.component.ts
│   │   │   │   ├── page-header.component.html
│   │   │   │   └── page-header.component.css
│   │   │   ├── data-table/
│   │   │   ├── confirm-dialog/
│   │   │   ├── empty-state/
│   │   │   ├── loading-spinner/
│   │   │   └── status-badge/
│   │   ├── directives/
│   │   │   ├── has-feature.directive.ts
│   │   │   └── has-permission.directive.ts
│   │   ├── pipes/
│   │   │   ├── arabic-date.pipe.ts
│   │   │   └── currency-ar.pipe.ts
│   │   └── models/
│   │       ├── api-response.model.ts
│   │       └── pagination.model.ts
│   │
│   ├── layout/                    ← هيكل الصفحة الرئيسية
│   │   ├── main-layout/
│   │   │   ├── main-layout.component.ts
│   │   │   ├── main-layout.component.html
│   │   │   └── main-layout.component.css
│   │   ├── sidebar/
│   │   │   ├── sidebar.component.ts
│   │   │   ├── sidebar.component.html
│   │   │   └── sidebar.component.css
│   │   ├── topbar/
│   │   │   ├── topbar.component.ts
│   │   │   ├── topbar.component.html
│   │   │   └── topbar.component.css
│   │   └── auth-layout/
│   │       ├── auth-layout.component.ts
│   │       ├── auth-layout.component.html
│   │       └── auth-layout.component.css
│   │
│   ├── store/                     ← إدارة الـ State (NgRx Signals)
│   │   ├── auth.store.ts
│   │   ├── feature-flags.store.ts
│   │   └── ui.store.ts
│   │
│   ├── features/                  ← وحدات الأعمال (Lazy Loaded)
│   │   ├── auth/
│   │   │   ├── pages/
│   │   │   │   └── login/
│   │   │   │       ├── login.component.ts
│   │   │   │       ├── login.component.html
│   │   │   │       └── login.component.css
│   │   │   ├── services/
│   │   │   │   └── auth-api.service.ts
│   │   │   └── auth.routes.ts
│   │   │
│   │   ├── dashboard/
│   │   │   ├── pages/dashboard/
│   │   │   ├── components/
│   │   │   ├── services/
│   │   │   └── dashboard.routes.ts
│   │   │
│   │   ├── catalog/
│   │   ├── inventory/
│   │   ├── pos/
│   │   ├── sales/
│   │   ├── purchasing/
│   │   ├── finance/
│   │   └── settings/
│   │
│   ├── app.routes.ts
│   ├── app.config.ts
│   └── app.component.ts
│
├── environments/
│   ├── environment.ts
│   └── environment.prod.ts
│
└── assets/
    ├── i18n/
    │   ├── ar.json
    │   └── en.json
    └── styles/
        ├── _variables.css
        └── styles.css
```

### قاعدة كل وحدة (feature) — نفس البنية دائماً

كل وحدة في مجلد `features/` تتبع نفس البنية الداخلية:

```
features/اسم-الوحدة/
├── pages/              ← صفحات الوحدة (كل صفحة مجلد مستقل)
│   └── اسم-الصفحة/
│       ├── اسم-الصفحة.component.ts
│       ├── اسم-الصفحة.component.html
│       └── اسم-الصفحة.component.css
├── components/         ← مكوّنات فرعية خاصة بهذه الوحدة فقط
├── services/           ← خدمات الـ API الخاصة بهذه الوحدة
└── اسم-الوحدة.routes.ts
```

---

## 5. قواعد المكوّنات

### 5.1 البنية الإلزامية لكل مكوّن

كل مكوّن بدون استثناء = **3 ملفات**:

| الملف | ما يحتويه |
|-------|-----------|
| `component.ts` | الـ Class + Metadata + منطق الـ Component فقط |
| `component.html` | قالب HTML بالكامل — لا template في الـ .ts |
| `component.css` | أنماط CSS خاصة بالمكوّن — Tailwind مسموح |

### 5.2 قالب الـ .ts الإلزامي

```typescript
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-example',
  standalone: true,
  imports: [CommonModule /* أضف PrimeNG modules هنا */],
  templateUrl: './example.component.html',     // ← دائماً templateUrl
  styleUrl:    './example.component.css',      // ← دائماً styleUrl
})
export class ExampleComponent implements OnInit {

  // Inject Services
  private readonly service = inject(ExampleService);

  // State
  items = signal<Item[]>([]);
  loading = signal(false);

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void { /* ... */ }
}
```

### 5.3 ما هو ممنوع منعاً باتاً

```typescript
// ❌ ممنوع — كتابة الـ HTML مباشرة داخل ملف .ts
@Component({
  template: `<div>...</div>`,
  // الصحيح: templateUrl: './example.component.html'
})

// ❌ ممنوع — كتابة الـ CSS مباشرة داخل ملف .ts
@Component({
  styles: [`.class { ... }`],
  // الصحيح: styleUrl: './example.component.css'
})

// ❌ ممنوع
const x: any = something;           // لا any كنوع بيانات

// ❌ ممنوع
import { OtherFeatureComponent } from '../other-feature/...';  // لا تستورد من feature أخرى مباشرة
```

---

## 6. نظام التصميم

### 6.1 لوحة الألوان — 3 ألوان فقط

> ⚠️ لا تضيف ألواناً جديدة بدون موافقة. المشروع يستخدم 3 ألوان رئيسية فقط.

| الاسم | الكود | كود Tailwind | الاستخدام |
|-------|-------|--------------|-----------|
| Primary — Navy | `#1E3A5F` | `bg-[#1E3A5F]` | Header، Sidebar، Buttons الرئيسية |
| Accent — Blue | `#2E86AB` | `bg-[#2E86AB]` | روابط، تمييز، Active states |
| Light — Sky | `#F0F7FF` | `bg-[#F0F7FF]` | خلفيات Cards، Hover states |
| Text Dark | `#1E293B` | `text-[#1E293B]` | النصوص الرئيسية |
| Text Muted | `#64748B` | `text-[#64748B]` | النصوص الثانوية والـ Labels |
| Border | `#CBD5E1` | `border-[#CBD5E1]` | حدود الجداول والـ Cards |
| White | `#FFFFFF` | `bg-white` | خلفية الصفحة والـ Cards |
| Success | — | `text-green-700 bg-green-50` | النجاح والحالة الإيجابية |
| Warning | — | `text-yellow-700 bg-yellow-50` | التحذيرات |
| Danger | — | `text-red-700 bg-red-50` | الأخطاء والتنبيهات الحرجة |

### 6.2 الأحجام — متوسطة دائماً

> التصميم متوسط الكثافة — لا كبير ولا صغير.

| العنصر | Class Tailwind |
|--------|---------------|
| Buttons | `px-4 py-2 text-sm font-medium rounded-lg` |
| Input Fields | `px-3 py-2 text-sm border rounded-lg w-full` |
| Table Header | `px-4 py-3 text-xs font-semibold uppercase tracking-wide` |
| Table Rows | `px-4 py-3 text-sm` |
| Cards | `p-4 rounded-xl border shadow-sm` |
| Page Title | `text-xl font-bold text-[#1E3A5F]` |
| Section Title | `text-base font-semibold text-[#1E3A5F]` |
| Labels | `text-xs font-medium text-[#64748B] uppercase tracking-wide` |
| Badge / Status | `px-2 py-0.5 text-xs font-medium rounded-full` |
| Sidebar Width | `w-64` (256px) |
| Topbar Height | `h-14` (56px) |
| Icons Size | `text-base` (16px) أو `text-lg` (18px) |

### 6.3 الأيقونات — PrimeIcons

> استخدم PrimeIcons لجميع الأيقونات. لا تخلط مكتبات مختلفة.

| العملية | Class الأيقونة |
|---------|---------------|
| عرض / تفاصيل | `pi pi-eye` |
| تعديل | `pi pi-pencil` |
| حذف | `pi pi-trash` |
| إضافة جديد | `pi pi-plus` |
| بحث | `pi pi-search` |
| فلترة | `pi pi-filter` |
| تصدير | `pi pi-download` |
| طباعة | `pi pi-print` |
| تحديث | `pi pi-refresh` |
| موافقة | `pi pi-check` |
| رفض | `pi pi-times` |
| المبيعات | `pi pi-shopping-cart` |
| المخزون | `pi pi-box` |
| المشتريات | `pi pi-truck` |
| المالية | `pi pi-wallet` |
| المستخدمين | `pi pi-users` |
| الإعدادات | `pi pi-cog` |
| الفروع | `pi pi-building` |
| POS | `pi pi-desktop` |
| التقارير | `pi pi-chart-bar` |
| الصلاحيات | `pi pi-shield` |
| الإشعارات | `pi pi-bell` |
| تسجيل خروج | `pi pi-sign-out` |
| القائمة | `pi pi-bars` |
| التاريخ | `pi pi-calendar` |
| جاري التحميل | `pi pi-spin pi-spinner` |

---

## 7. بنية الصفحات

### 7.1 صفحة القائمة (List Page)

كل صفحة تعرض قائمة بيانات تتبع هذا الترتيب الإلزامي:

```
1. Page Header   ← عنوان الصفحة + زر الإضافة
2. Filters Bar   ← حقل البحث + الفلاتر
3. Data Table    ← الجدول مع Pagination
```

**مثال HTML:**

```html
<!-- items-list.component.html -->

<div class="flex flex-col gap-4 p-4">

  <!-- 1. Page Header -->
  <app-page-header
    title="الأصناف"
    subtitle="إدارة وتصفح الأصناف والمنتجات"
    icon="pi pi-box">
    <button class="btn-primary">
      <i class="pi pi-plus text-sm"></i>
      إضافة صنف
    </button>
  </app-page-header>

  <!-- 2. Filters Bar -->
  <div class="card flex flex-wrap gap-3 items-end">
    <div class="flex-1 min-w-[200px]">
      <label class="field-label">بحث</label>
      <span class="p-input-icon-right w-full">
        <i class="pi pi-search"></i>
        <input pInputText placeholder="اسم الصنف أو الكود..." class="input-field" />
      </span>
    </div>
    <div class="w-48">
      <label class="field-label">الفئة</label>
      <p-dropdown [options]="categories" placeholder="الكل" class="w-full" />
    </div>
    <button class="btn-ghost">
      <i class="pi pi-filter-slash"></i> مسح
    </button>
  </div>

  <!-- 3. Data Table -->
  <div class="card p-0 overflow-hidden">
    <p-table
      [value]="items()"
      [loading]="loading()"
      [paginator]="true"
      [rows]="10"
      [rowsPerPageOptions]="[10, 25, 50]"
      [totalRecords]="totalRecords()"
      [lazy]="true"
      (onLazyLoad)="onLazyLoad($event)"
      styleClass="p-datatable-sm p-datatable-striped">

      <ng-template pTemplate="header">
        <tr>
          <th pSortableColumn="code" class="th-cell w-28">
            الكود <p-sortIcon field="code" />
          </th>
          <th pSortableColumn="name" class="th-cell">
            اسم الصنف <p-sortIcon field="name" />
          </th>
          <th class="th-cell w-24 text-center">الحالة</th>
          <th class="th-cell w-28 text-center">إجراءات</th>
        </tr>
      </ng-template>

      <ng-template pTemplate="body" let-item>
        <tr class="hover:bg-[#F0F7FF] transition-colors">
          <td class="td-cell font-mono text-xs">{{ item.code }}</td>
          <td class="td-cell font-medium">{{ item.name }}</td>
          <td class="td-cell text-center">
            <span [class]="getStatusClass(item.isActive)">
              {{ item.isActive ? 'نشط' : 'متوقف' }}
            </span>
          </td>
          <td class="td-cell text-center">
            <div class="flex justify-center gap-1">
              <button class="icon-btn" (click)="view(item)">
                <i class="pi pi-eye"></i>
              </button>
              <button class="icon-btn" (click)="edit(item)">
                <i class="pi pi-pencil"></i>
              </button>
              <button class="icon-btn danger" (click)="delete(item)">
                <i class="pi pi-trash"></i>
              </button>
            </div>
          </td>
        </tr>
      </ng-template>

      <ng-template pTemplate="emptymessage">
        <tr><td colspan="4"><app-empty-state /></td></tr>
      </ng-template>

    </p-table>
  </div>

</div>
```

### 7.2 صفحة النموذج (Form Page)

كل نموذج إضافة أو تعديل يتبع هذا الترتيب:

```
1. Page Header   ← عنوان النموذج + زر الرجوع
2. Form Card     ← النموذج مقسّم إلى أقسام منطقية
3. Form Actions  ← أزرار الحفظ والإلغاء في الأسفل
```

**مثال HTML:**

```html
<!-- item-form.component.html -->

<div class="flex flex-col gap-4 p-4">

  <!-- Page Header -->
  <app-page-header
    [title]="isEdit ? 'تعديل صنف' : 'إضافة صنف جديد'"
    icon="pi pi-box"
    [showBack]="true"
    (back)="goBack()" />

  <!-- Form Card -->
  <div class="card">
    <form [formGroup]="form" (ngSubmit)="submit()" class="flex flex-col gap-6">

      <!-- Section: Basic Info -->
      <div>
        <h3 class="section-title">المعلومات الأساسية</h3>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mt-3">

          <div class="field">
            <label class="field-label">اسم الصنف *</label>
            <input pInputText formControlName="name" class="input-field" />
            <small class="field-error" *ngIf="hasError('name')">
              {{ getError('name') }}
            </small>
          </div>

          <div class="field">
            <label class="field-label">الكود *</label>
            <input pInputText formControlName="code" class="input-field" />
          </div>

        </div>
      </div>

      <!-- Form Actions -->
      <div class="flex justify-end gap-3 pt-4 border-t border-[#CBD5E1]">
        <button type="button" class="btn-ghost" (click)="goBack()">إلغاء</button>
        <button type="submit" class="btn-primary" [disabled]="form.invalid || loading()">
          <i class="pi pi-spin pi-spinner text-sm" *ngIf="loading()"></i>
          {{ isEdit ? 'حفظ التعديلات' : 'إضافة الصنف' }}
        </button>
      </div>

    </form>
  </div>

</div>
```

---

## 8. CSS Classes العالمية

> هذه الـ Classes معرّفة في `styles.css` وتُستخدم في كل مكوّن. **لا تعرّف نفس الـ class مرتين.**

```css
/* ── Layout & Cards ─────────────────────────────── */
.card {
  @apply bg-white rounded-xl border border-[#CBD5E1] shadow-sm p-4;
}

/* ── Buttons ─────────────────────────────────────── */
.btn-primary {
  @apply flex items-center gap-2 px-4 py-2 bg-[#1E3A5F] text-white
         text-sm font-medium rounded-lg hover:bg-[#2E86AB]
         transition-colors duration-200 disabled:opacity-50;
}
.btn-secondary {
  @apply flex items-center gap-2 px-4 py-2 bg-[#2E86AB] text-white
         text-sm font-medium rounded-lg hover:bg-[#1E3A5F]
         transition-colors duration-200;
}
.btn-ghost {
  @apply flex items-center gap-2 px-4 py-2 bg-transparent text-[#64748B]
         text-sm font-medium rounded-lg border border-[#CBD5E1]
         hover:bg-[#F0F7FF] transition-colors duration-200;
}
.btn-danger {
  @apply flex items-center gap-2 px-4 py-2 bg-red-600 text-white
         text-sm font-medium rounded-lg hover:bg-red-700
         transition-colors duration-200;
}

/* ── Icon Buttons ────────────────────────────────── */
.icon-btn {
  @apply w-8 h-8 flex items-center justify-center rounded-lg
         text-[#64748B] hover:bg-[#F0F7FF] hover:text-[#1E3A5F]
         transition-all duration-200;
}
.icon-btn.danger {
  @apply hover:bg-red-50 hover:text-red-600;
}

/* ── Form Fields ─────────────────────────────────── */
.field        { @apply flex flex-col gap-1; }
.field-label  { @apply text-xs font-semibold text-[#64748B] uppercase tracking-wide; }
.field-error  { @apply text-xs text-red-600; }
.input-field  {
  @apply w-full px-3 py-2 text-sm border border-[#CBD5E1] rounded-lg
         text-[#1E293B] focus:outline-none focus:border-[#2E86AB]
         focus:ring-1 focus:ring-[#2E86AB] transition-all;
}

/* ── Table ───────────────────────────────────────── */
.th-cell {
  @apply px-4 py-3 text-xs font-semibold text-[#64748B]
         uppercase tracking-wide bg-[#F8FAFC] border-b border-[#CBD5E1];
}
.td-cell {
  @apply px-4 py-3 text-sm text-[#1E293B] border-b border-[#F1F5F9];
}

/* ── Section Title ───────────────────────────────── */
.section-title {
  @apply text-sm font-semibold text-[#1E3A5F] pb-2 border-b border-[#CBD5E1];
}

/* ── Status Badges ───────────────────────────────── */
.badge-success { @apply px-2 py-0.5 text-xs font-medium rounded-full bg-green-50 text-green-700; }
.badge-warning { @apply px-2 py-0.5 text-xs font-medium rounded-full bg-yellow-50 text-yellow-700; }
.badge-danger  { @apply px-2 py-0.5 text-xs font-medium rounded-full bg-red-50 text-red-700; }
.badge-info    { @apply px-2 py-0.5 text-xs font-medium rounded-full bg-blue-50 text-blue-700; }
.badge-neutral { @apply px-2 py-0.5 text-xs font-medium rounded-full bg-gray-100 text-gray-600; }
```

---

## 9. نظام Feature Flags

### كيف يعمل

بعد تسجيل الدخول، يرجع الـ Backend قائمة الميزات المفعّلة للمنشأة.
تُخزَّن في الـ Store وتُستخدم في كل مكان عبر `directive` أو `service`.

### قائمة الـ Feature Keys

| Feature Key | من يحتاجها |
|-------------|-----------|
| `POS_MODULE` | للجميع تقريباً |
| `EXPIRY_DATES` | صيدلية، سوبرماركت |
| `BATCH_NUMBERS` | صيدلية، مواد غذائية |
| `SERIAL_NUMBERS` | إلكترونيات |
| `PRODUCT_VARIANTS` | ملابس، أحذية |
| `MULTI_BARCODE` | هايبر، صيدلية |
| `MULTI_WAREHOUSE` | مول، هايبر |
| `STOCK_TRANSFERS` | مول، هايبر |
| `WHOLESALE_MODULE` | مول، هايبر، صيدلية |
| `COST_CENTERS` | مول، هايبر |
| `MULTI_BRANCH` | مول، هايبر |
| `PHARMACY_MODULE` | صيدلية |
| `ADVANCED_REPORTS` | مول، هايبر |

### الاستخدام في الـ Template

```html
<!-- إظهار قسم فقط إذا الميزة مفعّلة -->
<div *hasFeature="'EXPIRY_DATES'">
  <app-expiry-section />
</div>

<!-- إظهار عناصر متعددة -->
<ng-container *hasFeature="'PRODUCT_VARIANTS'">
  <app-variants-section />
  <app-variants-table />
</ng-container>
```

### الاستخدام في الـ TypeScript

```typescript
import { inject, computed } from '@angular/core';
import { FeatureService } from '@core/services/feature.service';

export class ItemFormComponent {
  private features = inject(FeatureService);

  // Computed — يتحدث تلقائياً
  hasVariants = computed(() => this.features.isEnabled('PRODUCT_VARIANTS'));
  hasExpiry   = computed(() => this.features.isEnabled('EXPIRY_DATES'));
  hasSerial   = computed(() => this.features.isEnabled('SERIAL_NUMBERS'));
}
```

---

## 10. الفرز والتصفية والصفحات المتعددة

### المبدأ الأساسي

> كل الفرز والتصفية والتصفح بالصفحات يتم من الـ **Backend (Server-Side)** — لا Client-Side أبداً.

### نموذج الـ Query لكل طلب

```typescript
// shared/models/pagination.model.ts

export interface PagedQuery {
  page:       number;      // رقم الصفحة (يبدأ من 1)
  pageSize:   number;      // عدد السجلات في الصفحة
  search?:    string;      // نص البحث
  sortField?: string;      // الحقل المرتب عليه
  sortOrder?: 1 | -1;      // 1 تصاعدي / -1 تنازلي
  [key: string]: any;      // فلاتر إضافية
}

export interface PagedResult<T> {
  data:         T[];
  totalRecords: number;
  page:         number;
  pageSize:     number;
}
```

### نموذج الـ Component لقائمة مع Lazy Load

```typescript
// items-list.component.ts

export class ItemsListComponent {
  private service = inject(ItemsService);

  items        = signal<Item[]>([]);
  totalRecords = signal(0);
  loading      = signal(false);

  query: PagedQuery = {
    page: 1, pageSize: 10, search: '', sortField: 'name', sortOrder: 1
  };

  // يُستدعى من p-table عند تغيير الصفحة أو الترتيب
  onLazyLoad(event: TableLazyLoadEvent): void {
    this.query = {
      ...this.query,
      page:      (event.first! / event.rows!) + 1,
      pageSize:  event.rows!,
      sortField: event.sortField as string ?? 'name',
      sortOrder: event.sortOrder as 1 | -1 ?? 1,
    };
    this.loadData();
  }

  // البحث ينتظر 400ms بعد آخر حرف (debounce)
  onSearch = debounce((term: string) => {
    this.query = { ...this.query, search: term, page: 1 };
    this.loadData();
  }, 400);

  private loadData(): void {
    this.loading.set(true);
    this.service.getAll(this.query).subscribe({
      next: result => {
        this.items.set(result.data);
        this.totalRecords.set(result.totalRecords);
      },
      error: err => console.error(err),
      complete: () => this.loading.set(false),
    });
  }
}
```

---

## 11. HTTP والـ API Services

### نموذج الـ Service القياسي

```typescript
// features/catalog/services/items.service.ts

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

@Injectable({ providedIn: 'root' })
export class ItemsService {
  private http = inject(HttpClient);
  private base = `${environment.apiUrl}/items`;

  getAll(query: PagedQuery): Observable<PagedResult<Item>> {
    const params = new HttpParams({ fromObject: query as any });
    return this.http.get<PagedResult<Item>>(this.base, { params });
  }

  getById(id: string): Observable<Item> {
    return this.http.get<Item>(`${this.base}/${id}`);
  }

  create(data: CreateItemDto): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(this.base, data);
  }

  update(id: string, data: UpdateItemDto): Observable<void> {
    return this.http.put<void>(`${this.base}/${id}`, data);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
```

### الـ Interceptors

| Interceptor | المهمة |
|-------------|--------|
| `auth.interceptor.ts` | يضيف `Authorization: Bearer {token}` لكل طلب |
| `tenant.interceptor.ts` | يضيف `X-Tenant-Id` header لكل طلب |
| `error.interceptor.ts` | يعالج الأخطاء 401/403/500 ويعرض Toast |

---

## 12. قواعد التسمية

| النوع | الصيغة | مثال |
|-------|--------|------|
| Components | kebab-case | `item-form.component.ts` |
| Services | kebab-case | `items.service.ts` |
| Models / Interfaces | PascalCase | `Item`, `CreateItemDto`, `PagedResult` |
| Signals | camelCase | `items`, `loading`, `totalRecords` |
| Methods | camelCase + فعل | `loadData()`, `onSubmit()`, `goBack()` |
| Routes files | kebab-case.routes.ts | `catalog.routes.ts` |
| Store files | kebab-case.store.ts | `auth.store.ts` |
| Guard files | kebab-case.guard.ts | `auth.guard.ts` |
| Directive files | kebab-case.directive.ts | `has-feature.directive.ts` |
| Pipe files | kebab-case.pipe.ts | `arabic-date.pipe.ts` |
| Selectors CSS | app- prefix | `app-item-form`, `app-page-header` |
| Enums | PascalCase | `UserType`, `FeatureKey`, `PaymentMethod` |
| Constants | UPPER_SNAKE_CASE | `API_URL`, `MAX_ITEMS` |

---

## 13. قائمة الوحدات وصفحاتها

| الوحدة | الصفحات | Feature Flag المطلوب |
|--------|---------|----------------------|
| `auth` | login | — |
| `dashboard` | dashboard | — |
| `catalog` | items-list, item-form, categories, brands, units | — |
| `inventory` | stock-overview, stock-transfers, adjustments, expiry-tracking | `MULTI_WAREHOUSE` / `EXPIRY_DATES` |
| `pos` | pos-screen, open-shift, close-shift | `POS_MODULE` |
| `sales` | invoices-list, invoice-form, quotations, returns, customers | — |
| `purchasing` | orders-list, order-form, invoices-list, invoice-form, suppliers, returns | — |
| `finance` | chart-of-accounts, journal-entries, receipt-vouchers, payment-vouchers, fiscal-years, reports | — |
| `settings` | branches, warehouses, pos-terminals, users, roles, feature-flags | — |

---

## 14. افعل ولا تفعل

| ✅ افعل | ❌ لا تفعل |
|---------|-----------|
| `standalone: true` لكل component | تستخدم NgModules |
| اكتب `templateUrl` يشير لملف `.html` خارجي | تكتب `template` مع الكود مباشرة في ملف `.ts` |
| اكتب `styleUrl` يشير لملف `.css` خارجي | تكتب `styles` مع الكود مباشرة في ملف `.ts` |
| استخدم Tailwind لكل التصميم | تكتب CSS مخصصاً إلا للضرورة |
| استخدم `signals()` للـ State | تستخدم `BehaviorSubject` للـ State المحلي |
| استخدم `inject()` بدل constructor DI | تضع كل شيء في constructor |
| Server-Side pagination دائماً | تحمّل كل البيانات وتقسّمها Client-Side |
| debounce للبحث (400ms) | تطلب API عند كل ضغطة حرف |
| `*hasFeature` للميزات الاختيارية | تعرض محتوى بدون التحقق من Flag |
| PrimeIcons للأيقونات | تخلط مكتبات أيقونات مختلفة |
| 3 ألوان فقط: `#1E3A5F / #2E86AB / #F0F7FF` | تضيف ألوان عشوائية |
| حجم متوسط: `text-sm`, `py-2`, `px-4` | تجعل العناصر كبيرة جداً أو صغيرة جداً |
| TypeScript strict types دائماً | تستخدم `any` كنوع بيانات |

---

## 15. إعدادات البيئة

```typescript
// environments/environment.ts
export const environment = {
  production:      false,
  apiUrl:          'http://localhost:5000/api',
  appName:         'ERP System',
  defaultLang:     'ar',
  defaultPageSize: 10,
};

// environments/environment.prod.ts
export const environment = {
  production:      true,
  apiUrl:          'https://api.yourdomain.com/api',
  appName:         'ERP System',
  defaultLang:     'ar',
  defaultPageSize: 10,
};
```

---

## 16. نظام الإشعارات (Toast Notifications)

### المبدأ الأساسي

> كل عملية في التطبيق — نجاح أو فشل أو تحذير — **يجب أن تُظهر إشعاراً (Toast)**.
> الإشعارات تظهر في **أسفل اليسار** من الشاشة دائماً، وتختفي تلقائياً بعد 4 ثوانٍ.
> لا يُسمح أبداً بإجراء عملية API بصمت بدون إشعار للمستخدم.

---

### 16.1 الإعداد في app.config.ts

أضف `MessageService` في الـ providers و `<p-toast>` في `app.component.html` مرة واحدة فقط:

```typescript
// app.config.ts
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [
    MessageService,   // ← مطلوب عالمياً
  ],
};
```

```html
<!-- app.component.html -->
<router-outlet />
<p-toast position="bottom-left" [baseZIndex]="9999" />
```

---

### 16.2 NotificationService — الخدمة المركزية

أنشئ هذه الخدمة مرة واحدة فقط واستخدمها في كل مكان.
**لا تستدعي `MessageService` مباشرة من الـ Components أبداً.**

**المسار:** `src/app/core/services/notification.service.ts`

```typescript
import { Injectable, inject } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private msg = inject(MessageService);

  // نجاح — تم الحفظ، تم التعديل، تم الحذف
  success(detail: string, summary: string = 'تمت العملية بنجاح'): void {
    this.msg.add({ severity: 'success', summary, detail, life: 4000, closable: true });
  }

  // خطأ — فشل الحفظ، خطأ من الـ API
  error(detail: string, summary: string = 'حدث خطأ'): void {
    this.msg.add({ severity: 'error', summary, detail, life: 6000, closable: true });
  }

  // تحذير — بيانات ناقصة، تحذير قبل حذف
  warning(detail: string, summary: string = 'تنبيه'): void {
    this.msg.add({ severity: 'warn', summary, detail, life: 5000, closable: true });
  }

  // معلومة — جاري المعالجة، تم النسخ
  info(detail: string, summary: string = 'معلومة'): void {
    this.msg.add({ severity: 'info', summary, detail, life: 4000, closable: true });
  }

  // معالجة أخطاء الـ API تلقائياً — استخدمها في كل error handler
  handleApiError(err: any): void {
    // 422 — validation errors من الـ Backend
    if (err?.status === 422 && err?.error?.errors) {
      const messages: string[] = Object.values(err.error.errors).flat() as string[];
      messages.forEach(m =>
        this.msg.add({ severity: 'error', summary: 'خطأ في البيانات', detail: m, life: 6000, closable: true })
      );
      return;
    }
    if (err?.status === 400) { this.error(err?.error?.message || 'البيانات المرسلة غير صحيحة'); return; }
    if (err?.status === 401) { this.error('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مجدداً', 'غير مصرح'); return; }
    if (err?.status === 403) { this.error('ليس لديك صلاحية لتنفيذ هذه العملية', 'وصول مرفوض'); return; }
    if (err?.status === 404) { this.error('العنصر المطلوب غير موجود', 'غير موجود'); return; }
    if (err?.status === 409) { this.error(err?.error?.message || 'السجل موجود مسبقاً أو يوجد تعارض في البيانات', 'تعارض'); return; }
    if (err?.status === 500) { this.error('حدث خطأ في الخادم. يرجى المحاولة لاحقاً', 'خطأ في الخادم'); return; }
    this.error(err?.error?.message || err?.message || 'حدث خطأ غير متوقع');
  }
}
```

---

### 16.3 رسائل الإشعارات القياسية

استخدم هذه الرسائل الموحّدة في كل أجزاء التطبيق:

| العملية | النوع | الرسالة |
|---------|-------|---------|
| إنشاء سجل | `success` | `تم إنشاء [اسم العنصر] بنجاح` |
| تعديل سجل | `success` | `تم تحديث [اسم العنصر] بنجاح` |
| حذف سجل | `success` | `تم حذف [اسم العنصر] بنجاح` |
| تفعيل عنصر | `success` | `تم تفعيل [اسم العنصر]` |
| إيقاف عنصر | `warning` | `تم إيقاف [اسم العنصر]` |
| نسخ إلى الحافظة | `info` | `تم النسخ إلى الحافظة` |
| تصدير ملف | `info` | `جاري تصدير الملف...` |
| نموذج غير صالح | `warning` | `يرجى تصحيح الأخطاء في النموذج` |
| انقطاع الاتصال | `error` | `تعذّر الاتصال بالخادم. تحقق من الاتصال بالإنترنت` |

---

### 16.4 الاستخدام في الـ Components

**إنشاء سجل:**

```typescript
export class ItemFormComponent {
  private service = inject(ItemsService);
  private notify  = inject(NotificationService);
  private router  = inject(Router);
  loading = signal(false);

  submit(): void {
    if (this.form.invalid) {
      this.notify.warning('يرجى تصحيح الأخطاء في النموذج');
      this.form.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    this.service.create(this.form.value).subscribe({
      next: () => {
        this.notify.success('تم إنشاء الصنف بنجاح');
        this.router.navigate(['/catalog/items']);
      },
      error: (err) => {
        this.notify.handleApiError(err);   // ← يتعامل مع كل الأخطاء تلقائياً
        this.loading.set(false);
      },
      complete: () => this.loading.set(false),
    });
  }
}
```

**تعديل سجل:**

```typescript
update(): void {
  this.loading.set(true);
  this.service.update(this.id, this.form.value).subscribe({
    next: () => {
      this.notify.success('تم تحديث الصنف بنجاح');
      this.router.navigate(['/catalog/items']);
    },
    error: (err) => {
      this.notify.handleApiError(err);
      this.loading.set(false);
    },
    complete: () => this.loading.set(false),
  });
}
```

**حذف سجل مع تأكيد:**

```typescript
confirmDelete(item: Item): void {
  this.confirmationService.confirm({
    message: `هل أنت متأكد من حذف "${item.name}"؟`,
    header: 'تأكيد الحذف',
    icon: 'pi pi-exclamation-triangle',
    acceptLabel: 'نعم، احذف',
    rejectLabel: 'إلغاء',
    acceptButtonStyleClass: 'p-button-danger',
    accept: () => {
      this.service.delete(item.id).subscribe({
        next: () => {
          this.notify.success('تم حذف الصنف بنجاح');
          this.loadData();
        },
        error: (err) => this.notify.handleApiError(err),
      });
    },
  });
}
```

---

### 16.5 معالجة الأخطاء في error.interceptor.ts

الـ Interceptor يعالج أخطاء 401 و 500 تلقائياً على مستوى التطبيق كله، دون الحاجة للتكرار في كل Component:

```typescript
// core/interceptors/error.interceptor.ts
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '@core/services/notification.service';
import { AuthService } from '@core/services/auth.service';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notify = inject(NotificationService);
  const auth   = inject(AuthService);
  const router = inject(Router);

  return next(req).pipe(
    catchError(err => {
      if (err.status === 401) {
        auth.logout();
        router.navigate(['/auth/login']);
        notify.error('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مجدداً', 'غير مصرح');
      }
      if (err.status === 500) {
        notify.error('حدث خطأ في الخادم. يرجى المحاولة لاحقاً', 'خطأ في الخادم');
      }
      // باقي الأخطاء تُمرَّر للـ Component ليتعامل معها بـ handleApiError()
      return throwError(() => err);
    })
  );
};
```

---

### 16.6 القاعدة الذهبية للإشعارات

```
✅  كل create / update / delete ناجح   → notify.success(...)
✅  كل error handler                   → notify.handleApiError(err)
✅  نموذج غير صالح عند الإرسال        → notify.warning(...)
✅  عمليات النسخ والتصدير والمعلومات  → notify.info(...)
✅  الإشعارات دائماً في أسفل اليسار   → position="bottom-left"
✅  استخدم NotificationService فقط    — لا تستدعي MessageService مباشرة

❌  لا تترك أي error callback فارغاً أو يكتفي بـ console.error فقط
❌  لا تعرض alert() أو confirm() المدمجة في المتصفح
❌  لا تكرر منطق معالجة الأخطاء في كل component — استخدم handleApiError()
```

---

## 📌 ملخص سريع — للذكاء الاصطناعي والمطورين الجدد

> اقرأ هذه النقاط قبل أي شيء:

```
1.  كل component = 3 ملفات: .ts + .html + .css — بدون استثناء
2.  standalone: true في كل component — بدون استثناء
3.  في ملف .ts اكتب templateUrl مع مسار .html و styleUrl مع مسار .css
4.  Tailwind CSS للتصميم — 3 ألوان: #1E3A5F / #2E86AB / #F0F7FF
5.  حجم العناصر متوسط: text-sm، py-2، px-4
6.  PrimeNG للمكوّنات + PrimeIcons للأيقونات
7.  الفرز والتصفية والصفحات: Server-Side دائماً
8.  Feature Flags: استخدم *hasFeature directive
9.  ضع كل ملف في مجلده الصحيح حسب هيكلية القسم 4
10. اتبع قواعد التسمية في القسم 12 بدقة
11. كل عملية API تحتاج إشعار Toast — استخدم NotificationService (القسم 16)
12. الإشعارات دائماً في أسفل اليسار — لا alert() ولا console.error فقط
```

---

*ERP System — Frontend Guidelines v1.0 | Angular · PrimeNG · Tailwind CSS*