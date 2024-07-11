using zms.Infrastructure.Utility.FileStorage;
using zms.Infrastructure.Utility.FileStorage.Locations;

namespace zms.Persistence.FileStorage.SmsService.Config
{
    /// <summary>
    /// Корневое расположение данных
    /// </summary>
    public class StorageLocation : RootLocation<StorageLocation>
    {
        public StorageLocation(string path, IFileDefinitionStore<StorageLocation> fileDefinitionStore, CompressorBase<StorageLocation> compressor) 
            : base(path, "SmsServiceFileStorage", fileDefinitionStore, compressor)
        {
            Report = new(this);
        }

        /// <summary>
        /// расположение отчетности
        /// </summary>
        public ReportLocation<StorageLocation> Report { get; }
    }
}
