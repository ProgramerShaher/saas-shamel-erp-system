using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Common.Behaviors
{
    /// <summary>
    /// سلوك الخط الأنبوبي المسؤول عن تسجيل كل طلب يمر عبر الـ MediatR.
    /// يسجل اسم الـ Request، وقت البدء، وقت الانتهاء، والوقت المستغرق (بالميلي ثانية).
    /// يُطبق قاعدة سادساً من الدستور: Logging البداية والنهاية والفشل.
    /// </summary>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// يعترض الطلب قبل وصوله للـ Handler، ويسجل وقت البدء والنهاية.
        /// </summary>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("═══ بدء معالجة الطلب: [{RequestName}] ═══", requestName);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();
                stopwatch.Stop();

                _logger.LogInformation(
                    "═══ تمت معالجة [{RequestName}] بنجاح | الوقت المستغرق: {ElapsedMilliseconds} ms ═══",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch
            {
                stopwatch.Stop();
                _logger.LogWarning(
                    "═══ فشل معالجة [{RequestName}] بعد {ElapsedMilliseconds} ms ═══",
                    requestName,
                    stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
