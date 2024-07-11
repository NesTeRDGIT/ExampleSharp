using zms.Infrastructure.Logging.Serilog;
using zms.Root.Module.Application;
using zms.Root.Module.SmsService;
using zms.Root.Module.SmsService.Beeline;
using zms.Root.Module.SmsService.Rostelecom;
using zms.Root.Worker.SmsService.Module;

namespace zms.Root.Worker.SmsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = SerilogFactory.Create(new LoggerConfiguration { IsConsole = true });
            logger.Information("Start SmsService");

            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureLogging((_, loggingBuilder) =>
                    {
                        loggingBuilder.ClearProviders();
                    })
                    .ConfigureAppConfiguration((host, builder) =>
                    {
                        var configuration = builder.Build();

                        //добавление параметров конфигурации через отдельные файлы (поддержка секретов Docker)
                        if (configuration["ConfigurationFilesPath"] != null)
                            builder.AddKeyPerFile(directoryPath: configuration["ConfigurationFilesPath"], optional: true);
                    })
                    .ConfigureServices((host, services) =>
                    {
                        //замена консольного логгера на логгер по конфигурации
                        logger = SerilogFactory.Create(host.Configuration);

                        //добавление проектов
                        services.AddApplication(host.Configuration, options =>
                        {
                            options.UsingSerilogLogger = logger;
                        });


                        var provider = (host.Configuration["SmsService:Provider"] ?? string.Empty).ToUpper();

                        switch (provider)
                        {
                            case "BEELINE": 
                                services.AddSmsService(host.Configuration, options => options.UseBeeline());
                                logger.Information("SmsService provider: Beeline");
                                break;
                            case "ROSTELECOM": 
                                services.AddSmsService(host.Configuration, options => options.UseRostelecom());
                                logger.Information("SmsService provider: Rostelecom");
                                break;
                            case "TEST": 
                                services.AddSmsService(host.Configuration, options => options.UseTest());
                                logger.Information("SmsService provider: Test");
                                break;
                            default:
                                throw new Exception("Не указан провайдер для сервиса");
                        }
                      
                        services.AddSmsServiceWorker(host.Configuration);
                        services.AddHostedService<Worker>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception e)
            {
                logger.Fatal(e, "SmsService cannot start");
            }
        }
    }
}
