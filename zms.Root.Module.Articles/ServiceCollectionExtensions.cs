using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using zms.Infrastructure.Utility.FileStorage.MicrosoftDependencyInjection;
using zms.Persistence.FileStorage.Articles;
using zms.Persistence.FileStorage.Articles.Config;
using zms.Persistence.SqlServer.EF.Articles;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Persistence.SqlServer.EF.Articles.QueryObject;
using zms.Persistence.SqlServer.EF.Articles.Repository;
using zms.Persistence.SqlServer.EF.FileStorage.Repository;
using zms.Support.Articles.Application.Interactor.ClientQuery.GetArticle;
using zms.Support.Articles.Application.Interactor.ClientQuery.GetArticleListItem;
using zms.Support.Articles.Application.Interactor.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Application.Interactor.ClientQuery.GetAttachments;
using zms.Support.Articles.Application.Interactor.Command.AddArticle;
using zms.Support.Articles.Application.Interactor.Command.AddAttachment;
using zms.Support.Articles.Application.Interactor.Command.PruneAttachment;
using zms.Support.Articles.Application.Interactor.Command.RemoveArticle;
using zms.Support.Articles.Application.Interactor.Command.RemoveAttachment;
using zms.Support.Articles.Application.Interactor.Command.UpdateArticle;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticle;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachments;
using zms.Support.Articles.Application.Use.Command.AddArticle;
using zms.Support.Articles.Application.Use.Command.AddAttachment;
using zms.Support.Articles.Application.Use.Command.PruneAttachment;
using zms.Support.Articles.Application.Use.Command.RemoveArticle;
using zms.Support.Articles.Application.Use.Command.RemoveAttachment;
using zms.Support.Articles.Application.Use.Command.UpdateArticle;
using zms.Support.Articles.Domain;

namespace zms.Root.Module.Articles
{
    /// <summary>
    /// Модуль статей
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить модуль статей
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <returns></returns>
        public static IServiceCollection AddArticles(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Articles");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            //регистрация подключения к БД
            services.AddDbContext<IUnitOfWork, ArticlesContext>(option => option.UseSqlServer(connectionString), ServiceLifetime.Scoped, ServiceLifetime.Singleton);

            //регистрация файлового хранилища
            services.AddFileStorage<StorageLocation>(options =>
            {
                options.Path = configuration["ArticlesFileStorage"];
                options.UseFileDefinitionStore<FileDefinitionStore<StorageLocation>>();
                options.CompressorNameCodePage = 866;
            });

            //регистрация доменных служб
            services.AddScoped<IIdGenerator, IdGenerator>();
            services.AddScoped<IAttachmentStorage, AttachmentStorage>();

            //регистрация служб хранилища
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IGetArticleQueryObject, GetArticleQueryObject>();
            services.AddScoped<IGetAttachmentDataQueryObject, GetAttachmentDataQueryObject>();
            services.AddScoped<IGetArticleListItemQueryObject, GetArticleListItemQueryObject>();
            services.AddScoped<IGetAttachmentsQueryObject, GetAttachmentsQueryObject>();


            //регистрация обработчиков команд
            services.AddScoped<IAddArticleCommandHandler, AddArticleCommandHandler>();
            services.AddScoped<IUpdateArticleCommandHandler, UpdateArticleCommandHandler>();
            services.AddScoped<IRemoveArticleCommandHandler, RemoveArticleCommandHandler>();
            services.AddScoped<IAddAttachmentCommandHandler, AddAttachmentCommandHandler>();
            services.AddScoped<IRemoveAttachmentCommandHandler, RemoveAttachmentCommandHandler>();
            services.AddScoped<IPruneAttachmentCommandHandler, PruneAttachmentCommandHandler>();

            //регистрация обработчиков запросов
            services.AddScoped<IGetArticleQueryHandler, GetArticleQueryHandler>();
            services.AddScoped<IGetAttachmentDataQueryHandler, GetAttachmentDataQueryHandler>();
            services.AddScoped<IGetAttachmentsQueryHandler, GetAttachmentsQueryHandler>();
            services.AddScoped<IGetArticleListItemQueryHandler, GetArticleListItemQueryHandler>();

            return services;
        }
    }
}