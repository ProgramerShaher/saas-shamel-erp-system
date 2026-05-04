using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ERPsystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileService> _logger;

        public FileService(IWebHostEnvironment environment, ILogger<FileService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<string> SaveBase64ImageAsync(string base64Data, string folderName, string fileNamePrefix, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(base64Data))
                return null;

            // التأكد من أن النص يمثل صورة Base64 صالحة
            var match = Regex.Match(base64Data, @"data:image/(?<type>.+?);base64,(?<data>.+)");
            if (!match.Success)
            {
                // إذا لم يكن يحتوي على data:image/png;base64, فربما يكون مساراً قديماً أو نص غير صالح
                if (base64Data.StartsWith("http") || base64Data.StartsWith("/"))
                {
                    return base64Data; // إعادة المسار كما هو إذا كان رابطاً
                }
                
                _logger.LogWarning("تنسيق الصورة غير صالح. لم يتم استخراج البيانات.");
                return null;
            }

            var extension = match.Groups["type"].Value;
            var base64String = match.Groups["data"].Value;

            // تحديد امتداد الملف
            var ext = extension.ToLower() switch
            {
                "jpeg" => ".jpg",
                "jpg" => ".jpg",
                "png" => ".png",
                "webp" => ".webp",
                "svg+xml" => ".svg",
                _ => ".png"
            };

            // تنظيف اسم الملف أو توليد GUID
            var cleanPrefix = string.IsNullOrWhiteSpace(fileNamePrefix) 
                ? Guid.NewGuid().ToString("N") 
                : Regex.Replace(fileNamePrefix, @"[^a-zA-Z0-9_-]", "").ToLower();

            var fileName = $"{cleanPrefix}-{Guid.NewGuid().ToString("N").Substring(0, 6)}{ext}";

            // تحديد مسار المجلد (wwwroot/uploads/foldername)
            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads", folderName);
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, fileName);

            try
            {
                // تحويل النص إلى مصفوفة بايتات وحفظه كملف
                byte[] imageBytes = Convert.FromBase64String(base64String);
                await File.WriteAllBytesAsync(filePath, imageBytes, cancellationToken);

                _logger.LogInformation("تم حفظ الصورة بنجاح: {FilePath}", filePath);

                // إرجاع الرابط النسبي للوصول للملف
                return $"/uploads/{folderName}/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء حفظ الصورة بصيغة Base64.");
                return null;
            }
        }

        public void DeleteFile(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl)) return;

            // تجاهل الروابط الخارجية
            if (fileUrl.StartsWith("http")) return;

            try
            {
                // تنظيف المسار النسبي للحصول على المسار الفعلي
                var relativePath = fileUrl.TrimStart('/');
                var filePath = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), relativePath);

                // تأمين: عدم السماح بحذف ملفات خارج مجلد الـ uploads
                if (!filePath.Contains(Path.Combine("wwwroot", "uploads")))
                {
                    _logger.LogWarning("محاولة حذف ملف خارج المجلد المسموح به: {FilePath}", filePath);
                    return;
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.LogInformation("تم حذف الملف بنجاح: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء محاولة حذف الملف {FileUrl}", fileUrl);
            }
        }
    }
}
