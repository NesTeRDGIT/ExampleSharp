using Microsoft.AspNetCore.Mvc;
using zms.Generic.SmsService.Application.Use.Command.AddMessage;
using zms.Generic.SmsService.Application.Use.Query.GetMessage;
using MessageProjection = zms.Generic.SmsService.Application.Use.Query.GetMessage.MessageProjection;

namespace zms.UI.ApiControllers.SmsService.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("SmsService/Message")]
    public class MessageController : ControllerBase
    {
        private readonly IAddMessageCommandHandler addMessageCommandHandler;
        private readonly IGetMessageQueryHandler getMessageQueryHandler;

        public MessageController(IAddMessageCommandHandler addMessageCommandHandler, IGetMessageQueryHandler getMessageQueryHandler)
        {
            this.addMessageCommandHandler = addMessageCommandHandler ?? throw new ArgumentNullException(nameof(addMessageCommandHandler));
            this.getMessageQueryHandler = getMessageQueryHandler ?? throw new ArgumentNullException(nameof(getMessageQueryHandler));
        }


        [HttpGet]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<IList<MessageProjection>>> Get([FromQuery]List<long> id)
        {
            var getMessageResponse = await getMessageQueryHandler.HandleAsync(new GetMessageQuery
            {
                Id = id
            });
            return Ok(getMessageResponse.Messages);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<List<long>>> Post(AddMessageCommand command)
        {
            var addMessageCommandResponse = await addMessageCommandHandler.HandleAsync(command);
            return Ok(addMessageCommandResponse.Id);
        }
    }
}