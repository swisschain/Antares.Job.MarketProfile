using Lykke.Job.MarketProfile.Settings.JobSettings;
using Lykke.Sdk.Settings;

namespace Lykke.Job.MarketProfile.Settings
{
    public class AppSettings : BaseAppSettings
    {
        public MarketProfileJobSettings MarketProfileJob { get; set; }
    }
}
