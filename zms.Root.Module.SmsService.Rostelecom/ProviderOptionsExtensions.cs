using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Infrastructure.External.SmsService.Rostelecom;
using zms.Infrastructure.External.SmsService.Rostelecom.Api;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Json;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request;

namespace zms.Root.Module.SmsService.Rostelecom
{
    /// <summary>
    /// Модуль отправки электронной почты
    /// </summary>
    public static class ProviderOptionsExtensions
    {
        /// <summary>
        /// Использовать провайдер Rostelecom
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ProviderOptions UseRostelecom(this ProviderOptions options)
        {
            options.Services.AddScoped<ISmsSender, SmsSender>();
            options.Services.AddScoped<ISmsStatusChecker, SmsStatusChecker>();
            options.Services.AddScoped<RostelecomService>();
            options.Services.AddScoped<RequestFactory>();
            options.Services.AddSingleton<RostelecomJsonSerializer>();
            options.Services.AddSingleton(CreateBeelineOptions(options.Configuration));
            return options;
        }

        private static RostelecomOptions CreateBeelineOptions(IConfiguration configuration)
        {
            var options = new RostelecomOptions();
            var rostelecomConfiguration = configuration.GetSection("SmsService:Providers:Rostelecom");
            var hostConfig = rostelecomConfiguration["Host"];
            if (hostConfig != null)
            {
                options.Host = hostConfig;
            }

            var userNameConfig = rostelecomConfiguration["UserName"];
            if (userNameConfig != null)
            {
                options.UserName = userNameConfig;
            }

            var userPasswordConfig = rostelecomConfiguration["UserPassword"];
            if (userPasswordConfig != null)
            {
                options.UserPassword = userPasswordConfig;
            }

            var senderConfig = rostelecomConfiguration["Sender"];
            if (senderConfig != null)
            {
                options.Sender = senderConfig;
            }

            var timeZoneConfig = rostelecomConfiguration["TimeZone"];
            if (timeZoneConfig != null)
            {
                options.TimeZone = Convert.ToInt32(timeZoneConfig);
            }

            return options;
        }
    }
}