using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Infrastructure.External.SmsService.Beeline;
using zms.Infrastructure.External.SmsService.Beeline.Api;
using zms.Infrastructure.External.SmsService.Beeline.Api.Json;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request;

namespace zms.Root.Module.SmsService.Beeline
{
    /// <summary>
    /// Модуль отправки СМС
    /// </summary>
    public static class ProviderOptionsExtensions
    {
        /// <summary>
        /// Использовать провайдер Beeline
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ProviderOptions UseBeeline(this ProviderOptions options)
        {
            options.Services.AddScoped<ISmsSender, SmsSender>();
            options.Services.AddScoped<ISmsStatusChecker, SmsStatusChecker>();
            options.Services.AddScoped<BeelineService>();
            options.Services.AddScoped<RequestFactory>();
            options.Services.AddSingleton<BeelineJsonSerializer>();
            options.Services.AddSingleton(CreateBeelineOptions(options.Configuration));
            return options;
        }

        private static BeelineOptions CreateBeelineOptions(IConfiguration configuration)
        {
            var options = new BeelineOptions();
            var beelineConfiguration = configuration.GetSection("SmsService:Providers:Beeline");
            var hostConfig = beelineConfiguration["Host"];
            if (hostConfig != null)
            {
                options.Host = hostConfig;
            }

            var userNameConfig = beelineConfiguration["UserName"];
            if (userNameConfig != null)
            {
                options.UserName = userNameConfig;
            }

            var userPasswordConfig = beelineConfiguration["UserPassword"];
            if (userPasswordConfig != null)
            {
                options.UserPassword = userPasswordConfig;
            }

            var senderConfig = beelineConfiguration["Sender"];
            if (senderConfig != null)
            {
                options.Sender = senderConfig;
            }

            return options;
        }
    }
}