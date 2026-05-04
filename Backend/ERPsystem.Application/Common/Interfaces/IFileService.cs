using System.Threading;
using System.Threading.Tasks;

namespace ERPsystem.Application.Common.Interfaces
{
    /// <summary>
    /// خدمة التعامل مع الملفات المرفوعة من الواجهة الأمامية.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// يستقبل نص صورة بصيغة Base64 ويحفظه كملف حقيقي في السيرفر ويعيد الرابط الخاص به.
        /// </summary>
        /// <param name="base64Data">نص الصورة (مثال: data:image/png;base64,...)</param>
        /// <param name="folderName">اسم المجلد المستهدف (مثل: tenants, users)</param>
        /// <param name="fileNamePrefix">اسم مميز للملف (مثل: اسم المنشأة أو الـ Slug)</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        /// <returns>رابط الملف النسبي ليتم حفظه في قاعدة البيانات (مثل: /uploads/tenants/filename.png)</returns>
        Task<string> SaveBase64ImageAsync(string base64Data, string folderName, string fileNamePrefix, CancellationToken cancellationToken = default);

        /// <summary>
        /// يحذف ملف من السيرفر.
        /// </summary>
        /// <param name="fileUrl">مسار الملف (مثل /uploads/tenants/filename.png)</param>
        void DeleteFile(string fileUrl);
    }
}
