using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachments;
using zms.Support.Articles.Application.Use.Command.AddAttachment;
using zms.Support.Identity.Domain.RoleDictionary.Article;
using zms.UI.ApiControllers.Common;

namespace zms.UI.ApiControllers.Articles.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Articles/Article/{articleId:long}/Attachment")]
    public class ArticleAttachmentController(
        IGetAttachmentsQueryHandler getAttachmentsQueryHandler,
        IAddAttachmentCommandHandler addAttachmentCommandHandler)
        : ControllerBase
    {
        private readonly IGetAttachmentsQueryHandler getAttachmentsQueryHandler = getAttachmentsQueryHandler ?? throw new ArgumentNullException(nameof(getAttachmentsQueryHandler));
        private readonly IAddAttachmentCommandHandler addAttachmentCommandHandler = addAttachmentCommandHandler ?? throw new ArgumentNullException(nameof(addAttachmentCommandHandler));


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<AttachmentProjection>>> Get(long articleId)
        {
            var response = await getAttachmentsQueryHandler.HandleAsync(new GetAttachmentsQuery
            {
                ArticleId = articleId
            });
            return this.OkResult(response.Attachments);
        }

        [HttpPost]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> Post(long articleId)
        {
            await using var memoryStream = new MemoryStream();
            var file = Request.Form.Files[0];
            await file.CopyToAsync(memoryStream);

            var response = await addAttachmentCommandHandler.HandleAsync(new AddAttachmentCommand
            {
                ArticleId = articleId,
                Name = file.FileName,
                Data = memoryStream.GetBuffer()
            });
            return this.Created(response.AttachmentId);
        }
    }
}
