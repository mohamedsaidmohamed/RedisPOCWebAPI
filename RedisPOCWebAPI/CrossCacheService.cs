using EasyCaching.Core;
using Microsoft.Extensions.Configuration;

namespace RedisPOCWebAPI
{
    public class CrossCacheService : ICrossCacheService
    {
        #region Dependencies
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties
        private readonly IHybridCachingProvider _hybrid;
        #endregion

        #region Constructor
        public CrossCacheService( IConfiguration configuration, IHybridProviderFactory hybridFactory)
        {
           // _hybrid = hybridFactory.GetHybridCachingProvider(CacheStore.All.ToString());
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public Task<T?> GetCacheAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateCacheAsync<T>(string cacheKey, Func<Task<T>> getResonse, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateCacheAsync(string keyPrefix)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SetCacheAsync<T>(string cacheKey, T value, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
