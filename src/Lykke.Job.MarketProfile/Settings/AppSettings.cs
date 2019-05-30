using JetBrains.Annotations;
using Lykke.Job.MarketProfile.Settings.JobSettings;
using Lykke.Sdk.Settings;

namespace Lykke.Job.MarketProfile.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public MarketProfileJobSettings MarketProfileJob { get; set; }
    }
}
