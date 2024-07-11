using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zms.Common.Application.Base.Cqrs.OfCollectionQuery;
using zms.Common.Application.LightRead;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem;
using zms.UI.ApiControllers.Common;

namespace zms.UI.ApiControllers.Articles.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Articles/ArticleList")]
    public class ArticleListItemController(IGetArticleListItemQueryHandler getArticleListItemQueryHandler)
        : ControllerBase
    {
        private readonly IGetArticleListItemQueryHandler getArticleListItemQueryHandler = getArticleListItemQueryHandler ?? throw new ArgumentNullException(nameof(getArticleListItemQueryHandler));
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CollectionResponse<ArticleListItem>>> Get(LightReadParams<ArticleListItem> lightReadParams, bool queryingPaginationMetadata)
        {
            var response = await getArticleListItemQueryHandler.HandleAsync(new GetArticleListItemQuery
            {
                QueryingPaginationMetadata = queryingPaginationMetadata,
                LightReadParams = lightReadParams
            });
            return this.OkResult(response);
        }
    }
}
