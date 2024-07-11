using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Exception;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Наименование вложения
    /// </summary>
    public class Name : ValueObject<Name>
    {
        private Name()
        {

        }

        public Name(string value) : this()
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (string.IsNullOrWhiteSpace(value))
                throw new BadValueDomainException("Наименование вложения не может быть пустым");
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; }

        protected override int GetValueHashCode()
        {
            return Value.GetHashCode();
        }

        protected override bool CompareValues(Name other)
        {
            return Value == other.Value;
        }
    }
}
