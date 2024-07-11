using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Persistence.SqlServer.EF.Articles.Converter
{
    /// <summary>
    /// Конвертер ArticleId в long?
    /// </summary>
    public class ArticleIdConverter : ValueConverter<ArticleId, long?>
    {
#pragma warning disable EF1001
        public ArticleIdConverter() : base(
            v => v == ArticleId.Default ? null : v.Value,
            v => v == null ? ArticleId.Default : new ArticleId(v.Value),
            convertsNulls: true)
        { }
#pragma warning restore EF1001
    }
}
