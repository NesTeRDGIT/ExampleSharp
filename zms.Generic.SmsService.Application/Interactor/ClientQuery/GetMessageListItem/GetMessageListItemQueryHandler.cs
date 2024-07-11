using zms.Common.Application.Base.Cqrs.OfCollectionQuery;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessageListItem
{
    public class GetMessageListItemQueryHandler(IGetMessageListItemQueryObject getMessageListItemQueryObject) : IGetMessageListItemQueryHandler
    {
        private readonly IGetMessageListItemQueryObject getMessageListItemQueryObject = getMessageListItemQueryObject ?? throw new ArgumentNullException(nameof(getMessageListItemQueryObject));

        public async Task<CollectionResponse<MessageProjection>> HandleAsync(GetMessageListItemQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));
            ArgumentNullException.ThrowIfNull(query.LightReadParams, nameof(query.LightReadParams));

            return query.QueryingPaginationMetadata
                ? new CollectionResponse<MessageProjection>(await getMessageListItemQueryObject.GetAsync(query.LightReadParams), await getMessageListItemQueryObject.CountAsync(query.LightReadParams))
                : new CollectionResponse<MessageProjection>(await getMessageListItemQueryObject.GetAsync(query.LightReadParams));


        }
    }
}
