using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Провайдер отправки СМС
    /// </summary>
    public class Provider : ValueObject<Provider>
    {
        private Provider()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Наименование</param>
        public Provider(string name) : this()
        {
            Name = name ?? string.Empty;
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        protected override int GetValueHashCode()
        {
            return Name.GetHashCode();
        }

        protected override bool CompareValues(Provider other)
        {
            return Name == other.Name;
        }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public static Provider Default => new (string.Empty);
    }
}
