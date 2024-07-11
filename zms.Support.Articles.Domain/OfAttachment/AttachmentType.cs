using zms.Common.SharedKernel.Base.Domain;

namespace zms.Support.Articles.Domain.OfAttachment
{
    /// <summary>
    /// Тип вложения
    /// </summary>
    public class AttachmentType : StaticReference<AttachmentType, string>
    {
        private AttachmentType()
        {

        }

        private AttachmentType(string value, string name) : this()
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public static AttachmentType Default => new("", "");

        /// <summary>
        /// Системное вложение
        /// </summary>
        /// <remarks>Вложение которое не видит пользователь как вложение, оно используется в системных целях(например в тексте статьи)</remarks>
        public static AttachmentType System => new("system", "Системное");
    }
}
