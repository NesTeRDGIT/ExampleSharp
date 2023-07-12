using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace zms.Root.Module.SmsService
{
    /// <summary>
    /// Параметры провайдера
    /// </summary>
    public class ProviderOptions
    {
        public ProviderOptions(IServiceCollection services, IConfiguration configuration)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Сервисы
        /// </summary>
        public IServiceCollection Services { get; }
        
        /// <summary>
        /// Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }
    }
}
