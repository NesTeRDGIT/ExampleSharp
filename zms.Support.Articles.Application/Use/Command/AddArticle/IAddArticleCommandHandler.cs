using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddArticle
{
    /// <summary>
    /// Обработчик команды добавления статьи
    /// </summary>
    public interface IAddArticleCommandHandler : ICommandHandler<AddArticleCommand, AddArticleResponse>;
}
