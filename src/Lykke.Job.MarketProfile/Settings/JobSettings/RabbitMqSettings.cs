using JetBrains.Annotations;

namespace Lykke.Job.MarketProfile.Settings.JobSettings
{
    [UsedImplicitly]
    public class RabbitMqSettings
    {
        public string QuotesConnectionString { get; set; }
        public string QuotesExchangeName { get; set; }
    }
}
