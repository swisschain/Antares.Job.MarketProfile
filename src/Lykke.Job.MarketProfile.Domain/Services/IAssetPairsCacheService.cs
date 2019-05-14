using System.Threading.Tasks;
using Lykke.Job.MarketProfile.Contract;
using Lykke.Job.QuotesProducer.Contract;

namespace Lykke.Job.MarketProfile.Domain.Services
{
    public interface IAssetPairsCacheService
    {
        Task InitCacheAsync(AssetPairPrice[] pairsToCache);
        Task UpdatePairAsync(QuoteMessage quote);
        AssetPairPrice[] GetAll();
    }
}
