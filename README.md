إليك كود `README.md` احترافي جاهز لوضعه في مستودع المشروع:

```markdown
# 🧾 ErpSystem - Multi-Tenant SaaS ERP & Accounting

A cloud-based **Enterprise Resource Planning (ERP)** system built with **.NET 8**, **Angular**, and **SQL Server**, following **Clean Architecture** and **CQRS**.  
It serves small to medium-sized businesses (grocery stores, pharmacies, restaurants, retail shops, etc.) with fully integrated **Point of Sale**, **Inventory**, **Purchases**, and **Double-Entry Accounting**.

---

## 🎯 Target Audience
- 🏪 Supermarkets, grocery stores
- 💊 Pharmacies
- 🍕 Restaurants & cafés
- 👗 Clothing & accessory shops
- 🛒 Wholesale & retail stores
- 🏢 Service offices (real estate, law firms, etc.)

---

## 🧩 Key Features

- **Multi-Tenant SaaS** – isolated data per tenant, single application instance.
- **POS (Point of Sale)** – fast cashier interface with barcode scanning & printing.
- **Inventory Management** – stock tracking, stock alerts, stocktaking, Excel import.
- **Purchases & Suppliers** – purchase orders, automated stock update, supplier balances.
- **Expense Tracking** – categorized expenses with automatic accounting entries.
- **Double-Entry Accounting** – chart of accounts, general ledger, trial balance, income statement, balance sheet.
- **Reports & Dashboards** – sales, profits, inventory, financial statements, export to Excel/PDF.
- **Role-Based Access Control** – Admin, Manager, Accountant, Cashier.
- **JWT Authentication** – secure token-based login.
- **Audit Logging** – all changes tracked.
- **RESTful API** – ready for mobile or third-party integrations.

---

## ⚙️ Technology Stack

| Layer               | Technology                                      |
|---------------------|--------------------------------------------------|
| **Backend**         | .NET 8, ASP.NET Core Web API, C# 12             |
| **Frontend**        | Angular (latest LTS)                             |
| **Database**        | SQL Server + Entity Framework Core               |
| **Architecture**    | Clean Architecture, CQRS (MediatR)               |
| **Validation**      | FluentValidation                                 |
| **Authentication**  | JWT Bearer tokens                                |
| **Logging**         | Serilog                                          |
| **Documentation**   | Swagger / OpenAPI                                |

---

## 📁 Clean Architecture Layer Structure

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
