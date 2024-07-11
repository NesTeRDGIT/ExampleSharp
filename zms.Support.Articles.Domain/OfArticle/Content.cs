using zms.Common.SharedKernel.Base.Domain;

namespace zms.Support.Articles.Domain.OfArticle
{
    /// <summary>
    /// Содержимое статьи
    /// </summary>
    public class Content : ValueObject<Content>
    {
        private Content()
        {

        }

        public Content(string value) : this()
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            Value = value;
        }

        /// <summary>
        /// Содержимое
        /// </summary>
        public string Value { get; }

        protected override int GetValueHashCode()
        {
            return Value.GetHashCode();
        }

        protected override bool CompareValues(Content other)
        {
            return Value == other.Value;
        }
    }
}
