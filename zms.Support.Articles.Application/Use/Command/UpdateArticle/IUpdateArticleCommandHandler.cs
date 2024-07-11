using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.UpdateArticle
{
    /// <summary>
    /// Обработчик команды обновления статьи
    /// </summary>
    public interface IUpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand>;
}
