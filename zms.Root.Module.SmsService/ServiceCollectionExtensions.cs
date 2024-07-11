using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetCategory;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessageListItem;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessageSummary;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetStatus;
using zms.Generic.SmsService.Application.Interactor.Command.AddMessage;
using zms.Generic.SmsService.Application.Interactor.Command.CheckStatusMessage;
using zms.Generic.SmsService.Application.Interactor.Command.SendMessage;
using zms.Generic.SmsService.Application.Interactor.Query.GetMessage;
using zms.Generic.SmsService.Application.Outside.Report;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetCategory;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetSentMessagesReport;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetStatus;
using zms.Generic.SmsService.Application.Use.Command.AddMessage;
using zms.Generic.SmsService.Application.Use.Command.CheckStatusMessage;
using zms.Generic.SmsService.Application.Use.Command.SendMessage;
using zms.Generic.SmsService.Application.Use.Query.GetMessage;
using zms.Generic.SmsService.Domain;
using zms.Infrastructure.Report.SmsService;
using zms.Persistence.SqlServer.EF.FileStorage.Context;
using zms.Persistence.SqlServer.EF.SmsService;
using zms.Persistence.SqlServer.EF.SmsService.Context;
using zms.Persistence.SqlServer.EF.SmsService.QueryObject.ClientQuery;
using zms.Persistence.SqlServer.EF.SmsService.Repository;
using zms.Infrastructure.Utility.FileStorage.MicrosoftDependencyInjection;
using zms.Persistence.SqlServer.EF.FileStorage.Repository;
using zms.Generic.SmsService.Application.Persistence.FileStorage;
using zms.Persistence.FileStorage.SmsService;
using zms.Persistence.FileStorage.SmsService.Config;
using zms.Generic.SmsService.Application.Interactor.Command.SetMessageError;
using zms.Generic.SmsService.Application.Use.Command.SetMessageError;

namespace zms.Root.Module.SmsService
{
    /// <summary>
    /// Модуль отправки электронной почты
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить модуль отправки электронной
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <param name="provider">Параметры провайдера</param>
        /// <returns></returns>
        public static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration configuration, Action<ProviderOptions> provider = null)
        {
            var connectionString = configuration.GetConnectionString("SmsService");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            //регистрация подключения к БД
            services.AddDbContext<IUnitOfWork, SmsServiceContext>(option => option.UseSqlServer(connectionString), ServiceLifetime.Transient, ServiceLifetime.Singleton);

            //регистрация БД для файлового хранилища (одна БД для всех файловых хранилищ)
            if (services.All(x => x.ServiceType != typeof(FileStorageContext)))
                services.AddDbContext<FileStorageContext>(option => option.UseSqlServer(configuration.GetConnectionString("FileStorage")), ServiceLifetime.Scoped, ServiceLifetime.Singleton);

            //регистрация файлового хранилища
            services.AddFileStorage<StorageLocation>(options =>
            {
                options.Path = configuration.GetValue<string>("SmsServiceFileStorage");
                options.UseFileDefinitionStore<FileDefinitionStore<StorageLocation>>();
            });

            //регистрация доменных служб
            services.AddScoped<IIdGenerator, IdGenerator>();

            //регистрация служб хранилища
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<Generic.SmsService.Application.Persistence.QueryObject.IGetMessageQueryObject, Persistence.SqlServer.EF.SmsService.QueryObject.GetMessageQueryObject>();
            services.AddScoped<IGetMessageQueryObject, GetMessageQueryObject>();
            services.AddScoped<IGetMessageListItemQueryObject, GetMessageListItemQueryObject>();
            services.AddScoped<GetMessageListItemQueryObject>();
            services.AddScoped<IMessageSummaryQueryObject, MessageSummaryQueryObject>();
            services.AddScoped<IGetSentMessagesReportItemQueryObject, GetSentMessagesReportItemQueryObject>();
            services.AddScoped<IReportTemplateGetter, ReportTemplateGetter>();
            services.AddScoped<IReportWriter, ReportWriter>();

            //регистрация служб приложения
            services.AddScoped<ISentMessagesReportBuilder, SentMessagesReportBuilder>();

            //регистрация обработчиков команд
            services.AddScoped<IAddMessageCommandHandler, AddMessageCommandHandler>();
            services.AddScoped<ISetMessageErrorCommandHandler, SetMessageErrorCommandHandler>();
            
            //регистрация обработчиков запросов
            services.AddScoped<IGetMessageQueryHandler, GetMessageQueryHandler>();
            services.AddScoped<IGetMessageListItemQueryHandler, GetMessageListItemQueryHandler>();
            services.AddScoped<IGetStatusQueryHandler, GetStatusQueryHandler>();
            services.AddScoped<IGetCategoryQueryHandler, GetCategoryQueryHandler>();
            services.AddScoped<Generic.SmsService.Application.Use.ClientQuery.GetMessage.IGetMessageQueryHandler, Generic.SmsService.Application.Interactor.ClientQuery.GetMessage.GetMessageQueryHandler>();
            services.AddScoped<IGetMessageSummaryQueryHandler, GetMessageSummaryQueryHandler>();
            services.AddScoped<IGetSentMessagesReportQueryHandler, GetSentMessagesReportQueryHandler>();



            //Если указан провайдер
            if (provider != null)
            {
                services.AddScoped<ISendMessageCommandHandler, SendMessageCommandHandler>();
                services.AddScoped<ICheckStatusMessageCommandHandler, CheckStatusMessageCommandHandler>();

                provider.Invoke(new ProviderOptions(services, configuration));
            }
            return services;
        }
    }
}