using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Exception;

namespace zms.Support.Articles.Domain.OfArticle
{
    /// <summary>
    /// Заголовок
    /// </summary>
    public class Title : ValueObject<Title>
    {
        private Title()
        {

        }

        public Title(string value) : this()
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (string.IsNullOrWhiteSpace(value))
                throw new BadValueDomainException("Заголовок не может быть пустым");
            Value = value;
        }

        public string Value { get; }

        protected override int GetValueHashCode()
        {
            return Value.GetHashCode();
        }

        protected override bool CompareValues(Title other)
        {
            return Value == other.Value;
        }
    }
}
