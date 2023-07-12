using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Common.Contact;
using zms.Common.SharedKernel.Exception;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Получатель
    /// </summary>
    public class Recipient : ValueObject<Recipient>
    {
        private Recipient()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Имя получателя</param>
        /// <param name="phone">Телефон получателя</param>
        /// <exception cref="BadValueDomainException"></exception>
        public Recipient(string name, PhoneNumber phone) : this()
        {
            ArgumentNullException.ThrowIfNull(phone, nameof(phone));
            if (phone == PhoneNumber.Default)
            {
                throw new NoValueDomainException("Номер телефона получателя не может быть пустым", nameof(phone));
            }

            Phone = phone;
            Name = name ?? string.Empty;
        }

        /// <summary>
        /// Имя получателя
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public PhoneNumber Phone { get; }

        protected override int GetValueHashCode()
        {
            return HashCode.Combine(Name, Phone);
        }

        protected override bool CompareValues(Recipient other)
        {
            return (Name, Phone) == (other.Name, other.Phone);
        }
    }
}
