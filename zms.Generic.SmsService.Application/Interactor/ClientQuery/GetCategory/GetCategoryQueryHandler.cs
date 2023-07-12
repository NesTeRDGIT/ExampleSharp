using zms.Generic.SmsService.Application.Use.ClientQuery.GetCategory;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetCategory
{
    public class GetCategoryQueryHandler : IGetCategoryQueryHandler
    {
        public Task<GetCategoryResponse> HandleAsync(GetCategoryQuery query)
        {
            return Task.FromResult(new GetCategoryResponse
            {
                Categories = Category.GetAll().Select(x=> new CategoryProjection
                {
                    Value = x.Value,
                    Name = x.Name
                }).ToList()
            });
        }
    }
}
