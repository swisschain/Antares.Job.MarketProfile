using System.Collections.Generic;
using Autofac;
using Lykke.Job.MarketProfile.Contract;

namespace Antares.Service.MarketProfile.Client
{
    public interface IMarketProfileClient
    {
        IAssetPair Get(string id);
        List<IAssetPair> GetAll();
    }
}
