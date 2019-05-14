using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.MarketProfile.Settings.JobSettings
{
    [UsedImplicitly]
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
        [AzureTableCheck]
        public string AssetPairsPricesConnString { get; set; }
    }
}
