using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Root.Module.SmsService;
using zms.Root.Worker.SmsService.TestMode;

namespace zms.Root.Worker.SmsService.Module
{
    /// <summary>
    /// Модуль отправки электронной почты
    /// </summary>
    public static class SmsServiceTestModeModule
    {
        /// <summary>
        /// Использовать тестовый провайдер
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ProviderOptions UseTest(this ProviderOptions options)
        {
            options.Services.AddScoped<ISmsSender, TestSmsSender>();
            options.Services.AddScoped<ISmsStatusChecker, TestSmsStatusChecker>();
            return options;
        }
    }
}