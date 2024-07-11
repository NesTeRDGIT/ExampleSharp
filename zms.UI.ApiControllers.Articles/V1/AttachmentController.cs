using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Application.Use.Command.AddAttachment;
using zms.Support.Articles.Application.Use.Command.PruneAttachment;
using zms.Support.Articles.Application.Use.Command.RemoveAttachment;
using zms.Support.Identity.Domain.RoleDictionary.Article;
using zms.UI.ApiControllers.Common;

namespace zms.UI.ApiControllers.Articles.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Articles/Attachment")]
    public class AttachmentController(
        IGetAttachmentDataQueryHandler getAttachmentDataQueryHandler,
        IAddAttachmentCommandHandler addAttachmentCommandHandler,
        IRemoveAttachmentCommandHandler removeAttachmentCommandHandler,
        IPruneAttachmentCommandHandler pruneAttachmentCommandHandler)
        : ControllerBase
    {
        private readonly IGetAttachmentDataQueryHandler getAttachmentDataQueryHandler = getAttachmentDataQueryHandler ?? throw new ArgumentNullException(nameof(getAttachmentDataQueryHandler));
        private readonly IAddAttachmentCommandHandler addAttachmentCommandHandler = addAttachmentCommandHandler ?? throw new ArgumentNullException(nameof(addAttachmentCommandHandler));
        private readonly IRemoveAttachmentCommandHandler removeAttachmentCommandHandler = removeAttachmentCommandHandler ?? throw new ArgumentNullException(nameof(removeAttachmentCommandHandler));
        private readonly IPruneAttachmentCommandHandler pruneAttachmentCommandHandler = pruneAttachmentCommandHandler ?? throw new ArgumentNullException(nameof(pruneAttachmentCommandHandler));

        [HttpGet("{attachmentId:long}/Data")]
        [ResponseCache(VaryByHeader = "Origin", Duration = 31536000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FileStreamResult> Data(long attachmentId)
        {
            var response = await getAttachmentDataQueryHandler.HandleAsync(new GetAttachmentDataQuery
            {
                Id = attachmentId
            });
            return File(response.Attachment.Stream, MimeTypes.Get(response.Attachment.Name));
        }

        [HttpGet("{attachmentId:long}/Download")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 31536000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FileStreamResult> Download(long attachmentId)
        {
            var response = await getAttachmentDataQueryHandler.HandleAsync(new GetAttachmentDataQuery
            {
                Id = attachmentId
            });
            return File(response.Attachment.Stream, MimeTypes.Get(response.Attachment.Name), response.Attachment.Name);
        }

        [HttpPost]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> Post()
        {
            await using var memoryStream = new MemoryStream();
            var file = Request.Form.Files[0];
            await file.CopyToAsync(memoryStream);

            var response = await addAttachmentCommandHandler.HandleAsync(new AddAttachmentCommand
            {
                Name = file.FileName,
                Data = memoryStream.GetBuffer()
            });
            return this.Created(response.AttachmentId);
        }

        [HttpDelete("{id:long}")]
        [AuthorizeRoles(ArticleAuthorizeList.Admin, ArticleAuthorizeList.Writer)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(long id)
        {
            await removeAttachmentCommandHandler.HandleAsync(new RemoveAttachmentCommand
            {
                Id = id
            });
            return NoContent();
        }


        [HttpDelete("Prune")]
        [AuthorizeRoles(ArticleAuthorizeList.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<long>> Prune()
        {
            var response = await pruneAttachmentCommandHandler.HandleAsync(new PruneAttachmentCommand());
            return this.OkResult(response.Count);
        }
    }
}
