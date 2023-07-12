using zms.Common.Application.Base.Cqrs.OfPaginationCollectionResponse;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessageListItem
{
    public class GetMessageListItemQueryHandler : IGetMessageListItemQueryHandler
    {
        private readonly IGetMessageListItemQueryObject getMessageListItemQueryObject;

        public GetMessageListItemQueryHandler(IGetMessageListItemQueryObject getMessageListItemQueryObject)
        {
            this.getMessageListItemQueryObject = getMessageListItemQueryObject ?? throw new ArgumentNullException(nameof(getMessageListItemQueryObject));
        }

        public async Task<GetMessageListItemResponse> HandleAsync(GetMessageListItemQuery query)
        {
            return new GetMessageListItemResponse
            {
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        Count = await getMessageListItemQueryObject.CountAsync(query.LightReadParams)
                    }
                },
                Data = await getMessageListItemQueryObject.GetAsync(query.LightReadParams)
            };
        }
    }
}
