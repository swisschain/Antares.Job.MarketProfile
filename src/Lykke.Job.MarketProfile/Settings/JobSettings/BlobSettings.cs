using JetBrains.Annotations;

namespace Lykke.Job.MarketProfile.Settings.JobSettings
{
    [UsedImplicitly]
    public class BlobSettings
    {
        public string Container { get; set; }
        public string Key { get; set; }
    }
}
