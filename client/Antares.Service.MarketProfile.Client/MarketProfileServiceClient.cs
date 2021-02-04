using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Antares.Service.MarketProfile.LykkeClient;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.MarketProfile.Contract;
using Lykke.Job.MarketProfile.NoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;
using LykkeMarketProfile = Antares.Service.MarketProfile.LykkeClient.LykkeMarketProfile;

namespace Antares.Service.MarketProfile.Client
{
    public class MarketProfileServiceClient : IMarketProfileServiceClient, IDisposable
    {
        private readonly MyNoSqlTcpClient _myNoSqlClient;

        private readonly IMyNoSqlServerDataReader<AssetPairPriceNoSql> _readerAssetPairNoSql;
        private readonly ILykkeMarketProfile _httpClient;
        private readonly ILog _log;

        public MarketProfileServiceClient(
            string myNoSqlServerReaderHost,
            string marketServiceHttpApiUrl,
            ILogFactory logFactory)
        {
            _log = logFactory.CreateLog(nameof(MarketProfileServiceClient));
            var host = Environment.GetEnvironmentVariable("HOST") ?? Environment.MachineName;
            _httpClient = new LykkeMarketProfile(new Uri(marketServiceHttpApiUrl));

            _myNoSqlClient = new MyNoSqlTcpClient(() => myNoSqlServerReaderHost, host);

            _readerAssetPairNoSql = new MyNoSqlReadRepository<AssetPairPriceNoSql>(_myNoSqlClient, AssetPairPriceNoSql.TableName);
        }

        public IMarketProfileServiceClient EventualCache => this;

        public ILykkeMarketProfile HttpClient => _httpClient;

        public void Start()
        {
            _myNoSqlClient.Start();

            var sw = new Stopwatch();
            sw.Start();

            int currentRetry = 0;
            var totalAmount = 1;

            for (; ; )
            {
                try
                {
                    var allPairs = _httpClient.ApiMarketProfileGet();
                    totalAmount = allPairs.Count;
                    break;
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    currentRetry++;

                    if (currentRetry > 10)
                    {
                        _log.Warning("Can not access market profile service to get amount of asset pairs");
                        break;
                    }
                }

                var delay = Math.Min((int)Math.Pow(2, currentRetry) * 100, 5000);
                Thread.Sleep(delay);
            }

            var isCacheSet = false;
            var iteration = 0;
            while (iteration < 100)
            {
                iteration++;
                if (EventualCache.GetAll().Count >= totalAmount)
                {
                    isCacheSet = true;
                    break;
                }

                Thread.Sleep(100);
            }
            sw.Stop();

            _log.Info($"MarketProfileServiceClient client is started. Wait time: {sw.ElapsedMilliseconds} ms");

            if (!isCacheSet)
            {
                _log.Warning($"Cache items amount = {EventualCache.GetAll().Count}, " +
                             $"while service items amount = {totalAmount}, They should be equal!");
            }
        }

        public void Dispose()
        {
            _myNoSqlClient.Stop();
        }

        IAssetPair IMarketProfileServiceClient.Get(string id)
        {
            try
            {
                var data = _readerAssetPairNoSql.Get(
                    AssetPairPriceNoSql.GeneratePartitionKey(),
                    AssetPairPriceNoSql.GenerateRowKey(id));

                return data?.AssetPair;
            }
            catch (Exception ex)
            {
                _log.Error(nameof(IMarketProfileServiceClient.Get), ex, $"Cannot read from MyNoSQL. Table: ${AssetPairPriceNoSql.TableName}, " +
                                                                   $"PK: {AssetPairPriceNoSql.GeneratePartitionKey()}, " +
                                                                   $"RK: {AssetPairPriceNoSql.GenerateRowKey(id)}, Ex: {ex}");
                throw;
            }
        }

        List<IAssetPair> IMarketProfileServiceClient.GetAll()
        {
            try
            {
                var data = _readerAssetPairNoSql.Get();

                return data.Select(e => (IAssetPair)e.AssetPair).ToList();
            }
            catch (Exception ex)
            {
                _log.Error(nameof(IMarketProfileServiceClient.GetAll), ex, $"Cannot read from MyNoSQL. Table: ${AssetPairPriceNoSql.TableName}, Ex: {ex}");
                throw;
            }
        }
    }
}
