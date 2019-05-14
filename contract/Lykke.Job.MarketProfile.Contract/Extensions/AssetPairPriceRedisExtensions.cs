using System;
using StackExchange.Redis;

namespace Lykke.Job.MarketProfile.Contract.Extensions
{
    public static class AssetPairPriceRedisExtensions
    {
        private const string DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        
        public static AssetPairPrice ToAssetPairPrice(this HashEntry[] hashEntry)
        {
            var assetPair = new AssetPairPrice();
            
            var hashDict = hashEntry.ToDictionary();

            if (hashDict.TryGetValue(nameof(AssetPairPrice.AssetPair), out var assetPairId))
                assetPair.AssetPair = assetPairId;
            
            if (hashDict.TryGetValue(nameof(AssetPairPrice.BidPrice), out var bidPrice))
                assetPair.BidPrice = (double)bidPrice;
            
            if (hashDict.TryGetValue(nameof(AssetPairPrice.BidPriceTimestamp), out var bidTimestamp))
                assetPair.BidPriceTimestamp = DateTime.Parse(bidTimestamp);
            
            if (hashDict.TryGetValue(nameof(AssetPairPrice.AskPrice), out var askPrice))
                assetPair.AskPrice = (double)askPrice;
            
            if (hashDict.TryGetValue(nameof(AssetPairPrice.AskPriceTimestamp), out var askTimestamp))
                assetPair.AskPriceTimestamp = DateTime.Parse(askTimestamp);
            
            return assetPair;
        }
        
        public static HashEntry[] ToHashEntries(this AssetPairPrice assetPair)
        {
            return new[]
            {
                new HashEntry(nameof(AssetPairPrice.AssetPair), assetPair.AssetPair),
                new HashEntry(nameof(AssetPairPrice.AskPrice), assetPair.AskPrice),
                new HashEntry(nameof(AssetPairPrice.AskPriceTimestamp), assetPair.AskPriceTimestamp.ToString(DateFormat)),
                new HashEntry(nameof(AssetPairPrice.BidPrice), assetPair.BidPrice),
                new HashEntry(nameof(AssetPairPrice.BidPriceTimestamp), assetPair.BidPriceTimestamp.ToString(DateFormat))
            };
        }
    }
}
