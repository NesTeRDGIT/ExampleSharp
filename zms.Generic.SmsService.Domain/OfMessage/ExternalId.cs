using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Внешний идентификатор
    /// </summary>
    public class ExternalId : ValueObject<ExternalId>
    {
        public ExternalId(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public static ExternalId Default => new (string.Empty);

        protected override int GetValueHashCode()
        {
            return Value.GetHashCode();
        }

        protected override bool CompareValues(ExternalId other)
        {
            return Value == other.Value;
        }

        public static implicit operator string(ExternalId value) => value.Value;
    }
}