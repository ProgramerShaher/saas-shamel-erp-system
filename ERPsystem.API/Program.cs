using Asp.Versioning;
using ERPsystem.API.Extensions;
using ERPsystem.API.Services;
using ERPsystem.Application;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Infrastructure;
using ERPsystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.AddSerilogLogging();

// Add services to the container.

builder.Services.AddControllers();

// تسجيل خدمات طبقة الـ Application (MediatR + Validators + Pipeline Behaviors)
builder.Services.AddApplicationServices();

// تسجيل خدمات طبقة الـ Infrastructure (الـ DbContext + المقابس الخارجية)
builder.Services.AddInfrastructureServices(builder.Configuration);

// تسجيل خدمات الـ API (المستخدم الحالي والـ HttpContext)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
// إعداد الإصدارات (API Versioning)
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure Global Exception Handler
app.ConfigureGlobalExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // تشغيل الـ Endpoint الخاص بملف الـ JSON
    app.MapOpenApi();

    // 3. تشغيل واجهة Scalar الرسومية
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ضخ البيانات الأولية (Seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // التأكد من تطبيق أي هجرات معلقة وضخ البيانات
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }
        await ApplicationDbContextSeed.SeedDatabaseAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "حدث خطأ أثناء ضخ البيانات الأولية (Seeding) في قاعدة البيانات.");
    }
}

await app.RunAsync();
