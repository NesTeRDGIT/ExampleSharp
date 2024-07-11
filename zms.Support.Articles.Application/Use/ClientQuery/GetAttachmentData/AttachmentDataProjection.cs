namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData
{
    /// <summary>
    /// Проекция вложения
    /// </summary>
    public class AttachmentDataProjection
    {
        /// <summary>
        /// Идентификатор фотографии
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Поток с данными
        /// </summary>
        public Stream Stream { get; set; }
    }
}
