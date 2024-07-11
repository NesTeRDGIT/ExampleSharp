using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.RemoveArticle
{
    /// <summary>
    /// Обработчик команды удаления статьи
    /// </summary>
    public interface IRemoveArticleCommandHandler : ICommandHandler<RemoveArticleCommand>;
}
