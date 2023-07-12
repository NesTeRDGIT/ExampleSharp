using zms.Generic.SmsService.Application.Use.ClientQuery.GetStatus;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetStatus
{
    public class GetStatusQueryHandler : IGetStatusQueryHandler
    {
        public Task<GetStatusResponse> HandleAsync(GetStatusQuery query)
        {
            return Task.FromResult(new GetStatusResponse
            {
                Statuses = Status.GetAll().Select(x=> new StatusProjection
                {
                    Value = x.Value,
                    Name = x.Name
                }).ToList()
            });
        }
    }
}
