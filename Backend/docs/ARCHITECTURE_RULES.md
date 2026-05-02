# الهوية العامة للمشروع وقواعد البناء (Architecture Guidelines)

تمثل هذه الوثيقة الدستور التقني لمشروع **SaaS Multi-Tenant ERP**. جميع القرارات التقنية والكتابات البرمجية يجب أن تتبع هذه القواعد بصرامة تامة دون أي استثناء.

المعمارية المعتمدة: **Clean Architecture + CQRS + MediatR + Vertical Slices** مع قاعدة بيانات **SQL Server**.

## أولاً: AutoMapper — قواعد الاستخدام
* **اتجاه Mapping الوحيد:** من `Entity` إلى `DTO` عند الإرجاع للـ Client. **يُمنع منعاً باتاً** استخدامه من `Command` إلى `Entity`، يجب أن يكون النقل يدوياً (Explicit) داخل الـ Handler ليكون المنطق واضحاً.
* **تنظيم الـ Profiles:** كل ميزة (Feature) يجب أن تملك Profile مستقل خاص بها (مثال: `GetTenantsMappingProfile`) ويكون موقعه **حصراً داخل مجلد الميزة نفسها**. وإذا كانت الميزة (مثل أوامر الإنشاء والتعديل Commands) لا تحتاج تحويل لـ DTO، فيُمنع إنشاء Profile فارغ منعاً للـ Dead Code.
* **Projection (IQueryable):** عند الاستعلام (Queries)، استخدم `ProjectTo` من AutoMapper مباشرة على `IQueryable` لمنع جلب بيانات غير ضرورية من قاعدة البيانات إلى الذاكرة.
* **الحقول المحسوبة:** يجب إدراج منطق الحسابات بشكل صريح في الـ Profile باستخدام `ConvertUsing` أو داخل الـ Handler ولا تتركه للتخمين.
* **Flattening:** يُستخدم بحذر شديد وفقط عندما يحقق وضوحاً كاملاً للـ Client.
* **Ignore الحساسيات:** يجب التأكد من عمل الم تجاهل (Ignore) لأي بيانات حساسة كـ `PasswordHash` في كل Profile بلا استثناء.

## ثانياً: CQRS و MediatR
* **تقسيم كامل:** كل طلب إما `Command` (تغيير للبيانات) أو `Query` (قراءة فقط). لا شيء في المنتصف.
* **لا للـ Repositories:** الـ Handlers تتحدث مع `DbContext` مباشرة. المنطق المشترك بين عدة Handlers ينقل إلى Domain Services (مثل `AccountingService`).
* **الـ Pipeline Behaviors:** تعمل بالترتيب التالي: `Logging` -> `Validation` -> `Transaction`.
* **التحقق من البيانات (Validation):** كل `Command` له `FluentValidation` خاص به يُسجل وينفذ بصمت تلقائياً عبر الـ Behavior.

## ثالثاً: قاعدة البيانات والـ Entities
* **الوراثة:** جميع الكيانات ترث من `TenantBaseEntity` عدا كيان `Tenant` و `AuditLog` (إذا كان شاملاً). 
* **المفاتيح الأساسية:** كلها من النوع `Guid`.
* **الحذف المنطقي (Soft Delete):** جميع عمليات الحذف يجب أن تكون `Soft Delete` وتُطبق تلقائياً داخل الـ DbContext عبر `SaveChanges / SaveChangesAsync` Override.
* **الـ TenantId:** يتم تعيينه آلياً دائماً عبر واجهة `ICurrentUserService` التي تقرأ من הـ JWT بمجمله، ويُمنع تمرير المُعرف يدوياً.
* **Global Query Filter:** يطبق في الـ `DbContext` لضمان التصفية المطلقة على أساس `TenantId` و `IsDeleted`.
* **التكوين المستقل:** كل Entity تملك ملف `IEntityTypeConfiguration` منفصل.

## رابعاً: معالجة الأخطاء
* **استرداد النتائج (Result Pattern):** جميع أخطاء العمليات التجارية المتوقعة (صنف نافذ، حد ائتمان متجاوز) ترجع بنتيجة `Result.Failure` وليس `Exception`.
* **الـ Exceptions:** تستخدم للأخطاء التقنية القاتلة حصراً، مثل انقطاع الاتصال بقاعدة البيانات.
* **المتحكمات (Controllers):** خالية تماماً من منطق الأعمال وتقوم بتمرير الطلبات لـ MediatR ومن ثم إرجاع النتائج بناءً على النمط فقط.

## خامساً: IQueryable Extensions
لأي تفلتر أو ربط كينونات متكرر (Includes / Filters)، سيتم بناءها كـ `Extension Methods` على طبقة `IQueryable` مما يمحو الحاجة لإنشاء Repositories لغرض فصل تكرار الـ Includes.

## سادساً: Logging والتوثيق — قواعد صارمة
* **شرح الملفات (File Summary):** بداية كل ملف تحوي XML Summary باللغة العربية تشرح الغرض العام من هذا الملف.
* **الكلاسات والحقول:** تملك XML Summary عربي دقيق وواضح لبيان الغرض التشغيلي.
* **المهام والميثودات (Methods):** توثق بـ XML Summary المدخلات للميثود، والمخرجات، ووظيفتها الجوهرية.
* **Logging باللغة العربية داخل Handlers:**
  - عند البدء: Log بمستوى `Information` (بدأ معالجة طلب X.. ).
  - عند الانتهاء: Log بمستوى `Information` (تمت المعالجة بنجاح..).
  - عند الفشل: Log بمستوى `Warning` أو `Error` يُذكر فيه السبب التفصيلي.
* **LoggingBehavior:** يسجل تلقائياً اسم الـ `Command`/`Query` ووقت تنفيذه بالميلي ثانية لكل الطلبات.

## سابعاً: المجلدات وهيكلتها (Vertical Slices) وتسميات الكلاسات
في طبقة الـ `Application` والـ `API` يتم بناء المجلدات بحسب الـ Feature وليس بحسب النمط التقني. 
مثلاً مجلد مخصص لـ `GetTenantById` يكون داخله (الـ Query والـ Handler والـ Validator والـ DTO والـ Mapping Profile الخاص به) جميعاً في مكان واحد.

* **تسمية كائنات الرد (DTOs):** يُعتمد المقطع `Dto` كلاحقة (Suffix) بحسب اسم الكائن (مثال: `SubscriptionPlanDto`، `TenantDto`)، ويُمنع نهائياً استخدام اللاحقة `Vm` (ViewModel).
* **تطابق الملفات مع الكلاسات:** يُلزم بأن يكون اسم الملف البرمجي (`.cs`) مطابقاً تماماً لاسم الكلاس الموجود بداخله لتجنب الفوضى وتسهيل تتبع الملفات.

## ثامناً: وصايا مقدسة للنظام لا يعذر فيها الإخلال
1. **لا تمرر `TenantId` يدوياً أبداً.** (يأتي من Token فقط).
2. **لا لعملية `AutoMapper` من Command إلى Entity** أياً كان السبب.
3. **لا تضع منطق أعمال (Business Logic) بتاتاً في Controllers أو DTOs**.
4. **تجنب جلب بيانات ضخمة** بالاعتماد على Projection من Database عبر `ProjectTo`.
5. **تجنب Includes المتكررة** بكتابة Extension Method.
6. **العمليات المخزنية:** كل حركة تمس المخزون تُسجّل حركة `StockMovement` بدون أية تجاوزات.
7. **العمليات المالية:** أي بيع / شراء / مصروف / أوتوماتيكياً يولد `JournalEntry` محاسبي مزدوج عبر `AccountingService`.
