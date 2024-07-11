using zms.Infrastructure.Utility.FileStorage.Locations;

namespace zms.Persistence.FileStorage.SmsService.Config
{
    /// <summary>
    /// Расположение отчетности
    /// </summary>
    public class ReportLocation<TRootLocation> : Location<TRootLocation> where TRootLocation : RootLocation<TRootLocation>
    {
        public ReportLocation(Location<TRootLocation> parentLocation) : base("Report", parentLocation)
        {
            Templates = new("Templates", this);
            Reports = new("Reports", this);
        }

        /// <summary>
        /// Расположение шаблонов отчетов
        /// </summary>
        public Location<TRootLocation> Templates { get; }

        /// <summary>
        /// Расположение готовых отчетов
        /// </summary>
        public MonthlyLocation<TRootLocation> Reports { get; }
    }
}
