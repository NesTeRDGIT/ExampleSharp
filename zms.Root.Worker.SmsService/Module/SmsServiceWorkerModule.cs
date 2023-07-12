namespace zms.Root.Worker.SmsService.Module
{
    public static class SmsServiceWorkerModule
    {
        /// <summary>
        /// Универсальная регистрация служб SmsServiceWorker
        /// </summary>
        public static IServiceCollection AddSmsServiceWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SmsService>();
            services.AddSingleton(CreateSmsServiceOptions(configuration));
            return services;
        }


        /// <summary>
        /// Создание параметров сервиса
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static SmsServiceOptions CreateSmsServiceOptions(IConfiguration configuration)
        {
            var result = new SmsServiceOptions();
            var timeOutParameter = configuration["SmsService:TimeOut"];
            if (timeOutParameter != null)
            {
                result.TimeOut = Convert.ToInt32(timeOutParameter);
            }

            return result;
        }
    }
}
