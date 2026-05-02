
<a name="readme-top"></a>

# 🧾 ErpSystem — Multi-Tenant SaaS ERP & Accounting

نظام تخطيط موارد المؤسسات (ERP) سحابي متعدد المستأجرين، يدمج بين **إدارة المبيعات، المشتريات، المخزون، المصروفات** و**المحاسبة المالية المزدوجة**.  
مبني وفق **Clean Architecture** و **CQRS** باستخدام **.NET 8** و **Angular** و **SQL Server**.

---

## جدول المحتويات

- [الجمهور المستهدف](#-الجمهور-المستهدف)
- [الميزات الرئيسية](#-الميزات-الرئيسية)
- [التقنيات المستخدمة](#-التقنيات-المستخدمة)
- [الهيكل المعماري (Clean Architecture)](#-الهيكل-المعماري-clean-architecture)
- [آلية تعدد المستأجرين (Multi-Tenancy)](#-آلية-تعدد-المستأجرين-multi-tenancy)
- [أحداث المجال (Domain Events)](#-أحداث-المجال-domain-events)
- [المتطلبات الأساسية](#-المتطلبات-الأساسية)
- [تشغيل المشروع محلياً](#-تشغيل-المشروع-محلياً)
- [التوثيق (API Documentation)](#-التوثيق-api-documentation)
- [التقارير والمخرجات](#-التقارير-والمخرجات)
- [الاختبارات](#-الاختبارات)
- [النشر (Deployment)](#-النشر-deployment)
- [المساهمة](#-المساهمة)
- [الترخيص](#-الترخيص)
- [تواصل](#-تواصل)

---

## 🎯 الجمهور المستهدف

| الفئة | أمثلة |
|-------|-------|
| 🏪 بقالة وسوبر ماركت | بقالة الحي، سوبر ماركت متوسط |
| 💊 صيدليات | صيدلية مستقلة أو سلسلة صغيرة |
| 🍕 مطاعم وكافيهات | مطعم، كافيه، بيتزا |
| 👗 محلات ملابس وإكسسوارات | محل ألبسة، أحذية |
| 🛒 تجارة الجملة والتجزئة | مواد غذائية، منظفات |
| 🏢 مكاتب خدمات | عقارات، محاماة، شركات صغيرة |

---

## 🧩 الميزات الرئيسية

- **نظام متعدد المستأجرين (SaaS)** – عزل تام للبيانات بين المستأجرين.
- **واجهة كاشير (POS)** – بحث سريع بالباركود، سلة مشتريات، طباعة الفاتورة.
- **إدارة المخزون** – حركة آنية، تنبيهات النفاد، جرد، استيراد Excel.
- **إدارة المشتريات والموردين** – فواتير شراء، تحديث آلي للمخزون.
- **إدارة المصروفات** – تصنيف المصروفات وربطها المحاسبي.
- **محاسبة مزدوجة كاملة** – شجرة حسابات، قيود تلقائية، دفتر أستاذ، ميزان مراجعة، قائمة دخل، ميزانية عمومية.
- **تقارير ولوحة تحكم** – المبيعات، الأرباح، المخزون، وتقارير مالية مع تصدير Excel/PDF.
- **صلاحيات الأدوار (RBAC)** – مدير، محاسب، كاشير.
- **تسجيل الدخول بـ JWT** – آمن وقابل للتجديد.
- **سجل التدقيق (Audit Log)** – تتبع جميع التغييرات.
- **API موثقة بـ Swagger** – جاهزة لتطبيق جوال أو تكاملات خارجية.

---

## ⚙️ التقنيات المستخدمة

| الطبقة | التقنية |
|--------|---------|
| **الخادم (Backend)** | .NET 8, ASP.NET Core Web API, C# 12 |
| **الواجهة الأمامية (Frontend)** | Angular (آخر إصدار LTS) |
| **قاعدة البيانات** | SQL Server + Entity Framework Core |
| **المعمارية** | Clean Architecture + CQRS (MediatR) |
| **التحقق** | FluentValidation |
| **التسجيل والأمان** | JWT Bearer, Serilog |
| **التوثيق** | Swagger / OpenAPI |

---

## 📁 الهيكل المعماري (Clean Architecture)
```
src/
├── ErpSystem.Domain/          # Entities, Value Objects, Domain Events, Interfaces
├── ErpSystem.Application/     # CQRS Commands/Queries, DTOs, Validators, Behaviours
├── ErpSystem.Infrastructure/  # EF Core DbContext, Repositories, Services, JWT, Email
├── ErpSystem.API/             # Controllers, Middleware, Filters, Program.cs
└── ErpSystem.Tests/           # Unit & Integration tests
```

---

## 🧱 Multi-Tenancy Design

- Every business table includes `TenantId` (FK) referencing `Tenants`.
- Global Query Filter (`EF Core`) automatically filters data per tenant.
- Tenant resolution via JWT claim `tenant_id`.
- Admin dashboard for tenant registration & subscription management.

---

## 🧪 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (LTS)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or local)
- [Angular CLI](https://angular.io/cli)

### Backend Setup

```bash
# Clone repository
git clone https://github.com/your-org/ErpSystem.git
cd ErpSystem/src/ErpSystem.API

# Configure connection string in appsettings.json
# "ConnectionStrings": { "DefaultConnection": "Server=.;Database=ErpSystemDb;Trusted_Connection=True;" }

# Apply migrations
dotnet ef database update

# Run API
dotnet run
```

API available at `https://localhost:5001/swagger`

### Frontend Setup

```bash
cd src/frontend/erp-system-ui

# Install dependencies
npm install

# Run development server
ng serve
```

Open `http://localhost:4200`

---

## 📚 API Documentation

Swagger UI is available at `/swagger` when running in Development mode.  
Example endpoints:

- `POST /api/auth/login`
- `GET /api/products`
- `POST /api/sales`
- `GET /api/accounting/trial-balance`

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## 🚀 Deployment

Build and publish for production:

```bash
dotnet publish -c Release -o out
ng build --prod
```

Host on **Azure**, **AWS**, or your own server with SQL Server.

---

## 🤝 Contributing

Contributions are welcome! Please follow the Clean Architecture & CQRS patterns, write tests, and document new endpoints.

---

## 📄 License

[MIT](LICENSE) or your preferred license.

---

## 📬 Contact

Developer: [Shaher Alyaari]  
Email: [shaheralyaari@gmail.com]  
```

---

هذا `README.md` يغطي وصف المشروع، الجمهور المستهدف، الميزات، التقنيات، الهيكل، تعليمات التشغيل، والمساهمة. يمكنك تعديل الروابط والمعلومات الشخصية حسب مشروعك. جاهز للنسخ مباشرة.
