using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticle;
using zms.Support.Articles.Application.Use.Command.AddArticle;
using zms.Support.Articles.Application.Use.Command.RemoveArticle;
using zms.Support.Articles.Application.Use.Command.UpdateArticle;
using zms.Support.Identity.Domain.RoleDictionary.Article;
using zms.UI.ApiControllers.Articles.V1.Model;
using zms.UI.ApiControllers.Common;

namespace zms.UI.ApiControllers.Articles.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Articles/Article")]
    public class ArticleController(
        IGetArticleQueryHandler getArticleQueryHandler,
        IAddArticleCommandHandler addArticleCommandHandler,
        IUpdateArticleCommandHandler updateArticleCommandHandler,
        IRemoveArticleCommandHandler removeArticleCommandHandler)
        : ControllerBase
    {
        private readonly IGetArticleQueryHandler getArticleQueryHandler = getArticleQueryHandler ?? throw new ArgumentNullException(nameof(getArticleQueryHandler));
        private readonly IAddArticleCommandHandler addArticleCommandHandler = addArticleCommandHandler ?? throw new ArgumentNullException(nameof(addArticleCommandHandler));
        private readonly IUpdateArticleCommandHandler updateArticleCommandHandler = updateArticleCommandHandler ?? throw new ArgumentNullException(nameof(updateArticleCommandHandler));
        private readonly IRemoveArticleCommandHandler removeArticleCommandHandler = removeArticleCommandHandler ?? throw new ArgumentNullException(nameof(removeArticleCommandHandler));

        [HttpGet("{id:long}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ArticleProjection>> Get(long id)
        {
            var response = await getArticleQueryHandler.HandleAsync(new GetArticleQuery
            {
                Id = id
            });
            return this.OkResult(response.Article);
        }

        [HttpPost]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> Post(ArticleModel model)
        {
            var response = await addArticleCommandHandler.HandleAsync(new AddArticleCommand
            {
                Title = model.Title,
                Content = model.Content,
                AttachAttachments = model.AttachAttachments
            });
            return  this.Created(response.ArticleId);
        }

        [HttpPatch("{id:long}")]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<long>> Patch(long id, ArticleModel model)
        {
            await updateArticleCommandHandler.HandleAsync(new UpdateArticleCommand
            {
                Id = id,
                Title = model.Title,
                Content = model.Content,
                AttachAttachments = model.AttachAttachments
            });
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(long id)
        {
            await removeArticleCommandHandler.HandleAsync(new RemoveArticleCommand
            {
                Id = id
            });
            return NoContent();
        }
    }
}
