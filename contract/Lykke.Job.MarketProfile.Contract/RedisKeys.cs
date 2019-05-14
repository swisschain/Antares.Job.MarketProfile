namespace Lykke.Job.MarketProfile.Contract
{
    public static class RedisKeys
    {
        public static string GetMarketProfileKey(string assetPairId) => $"MarketProfile:AssetPair:{assetPairId}";
        public static string GetAssetPairsKey() => "MarketProfile:AssetPairs";
    }
}
