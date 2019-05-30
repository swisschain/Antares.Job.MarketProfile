using System;
using Lykke.Job.MarketProfile.Contract;

namespace Lykke.Job.MarketProfile.Domain.Repositories
{
    public class AssetPair
    {
        public string Code { get; set; }
        public double BidPrice { get; set; }
        public double AskPrice { get; set; }
        public DateTime BidPriceTimestamp { get; set; }
        public DateTime AskPriceTimestamp { get; set; }

        public AssetPairPrice ToAssetPairPrice()
        {
            return new AssetPairPrice
            {
                AssetPair = Code,
                BidPrice = BidPrice,
                BidPriceTimestamp = BidPriceTimestamp,
                AskPrice = AskPrice,
                AskPriceTimestamp = AskPriceTimestamp
            };
        }
        
        public static AssetPair Create(AssetPairPrice pair)
        {
            return new AssetPair
            {
                Code = pair.AssetPair,
                BidPrice = pair.BidPrice,
                BidPriceTimestamp = pair.BidPriceTimestamp,
                AskPrice = pair.AskPrice,
                AskPriceTimestamp = pair.AskPriceTimestamp
            };
        }
    }
}
