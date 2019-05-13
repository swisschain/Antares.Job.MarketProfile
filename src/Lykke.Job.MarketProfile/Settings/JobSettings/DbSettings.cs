using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.MarketProfile.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
