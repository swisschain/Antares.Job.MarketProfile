using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.MarketProfile.Settings.JobSettings
{
    [UsedImplicitly]
    public class RabbitMqSettings
    {
        public string QuotesConnectionString { get; set; }
        public string QuotesExchangeName { get; set; }
        [Optional]
        public string QueueSuffix { get; set; }
    }
}
