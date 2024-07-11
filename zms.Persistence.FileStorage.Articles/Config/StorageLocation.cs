using zms.Infrastructure.Utility.FileStorage;
using zms.Infrastructure.Utility.FileStorage.Locations;

namespace zms.Persistence.FileStorage.Articles.Config
{
    /// <summary>
    /// Расположение данных
    /// </summary>
    public class StorageLocation : RootLocation<StorageLocation>
    {
        public StorageLocation(string path, IFileDefinitionStore<StorageLocation> fileDefinitionStore, CompressorBase<StorageLocation> compressor)
            : base(path, "ArticlesFileStorage", fileDefinitionStore, compressor)
        {
            Attachments = new Location<StorageLocation>("Attachments", this);
        }

        /// <summary>
        /// Каталог вложений
        /// </summary>
        public Location<StorageLocation> Attachments { get; }
    }
}
