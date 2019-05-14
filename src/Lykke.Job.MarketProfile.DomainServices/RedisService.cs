using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Job.MarketProfile.Contract;
using Lykke.Job.MarketProfile.Contract.Extensions;
using StackExchange.Redis;

namespace Lykke.Job.MarketProfile.DomainServices
{
    public class RedisService
    {
        private readonly IDatabase _database;

        public RedisService(
            IDatabase database
        )
        {
            _database = database;
        }

        public Task AddAssetPairPriceAsync(AssetPairPrice assetPair)
        {
            var tasks = new List<Task>
            {
                _database.HashSetAsync(RedisKeys.GetMarketProfileKey(assetPair.AssetPair), assetPair.ToHashEntries()),
                _database.SortedSetAddAsync(RedisKeys.GetAssetPairsKey(), assetPair.AssetPair, 0)
            };

            return Task.WhenAll(tasks);
        }
    }
}
