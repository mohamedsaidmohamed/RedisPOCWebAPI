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
           _hybrid = hybridFactory.GetHybridCachingProvider("SafeerAll");
            _configuration = configuration;
        }
        #endregion
        #region Methods
        public async Task<T?> GetCacheAsync<T>(string cacheKey,
                                             CancellationToken cancellationToken = default) where T : class
        {
            T? value = null;
            cacheKey = $"Safeer:{cacheKey}";
            await Task.Run(() => value = _hybrid.GetAsync<T>(cacheKey, cancellationToken).Result.Value);

            return value;
        }
        public async Task SetCacheAsync<T>(string cacheKey,
                                          T value,
                                          CancellationToken cancellationToken = default) where T : class
        {
            cacheKey = $"Safeer:{cacheKey}";
            await _hybrid.SetAsync(cacheKey, value, TimeSpan.FromMinutes(1), cancellationToken);

        }


        public async Task<T> GetOrCreateCacheAsync<T>(string cacheKey,
                                                      Func<Task<T>> getResonse,
                                                      CancellationToken cancellationToken = default) where T : class, new()
        {
            T? response = null;

            //if (!_configuration.CrossCacheEntry_IsEnabled)
            //{
            //    return await getResonse() ?? new();
            //}

            response = await GetCacheAsync<T>(cacheKey, cancellationToken);

            if (response is null)
            {
             //   _loggingService.Information($"Caching: {cacheKey} not found in cache. Fetching from database.");
                response = await getResonse();

                if (response != default)
                {
                    await SetCacheAsync(cacheKey, response, cancellationToken);
                }
            }
            else
            {
              //  _loggingService.Information($"Caching: {cacheKey} found in cache.");
            }

            return response ?? new();
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _hybrid.RemoveByPrefixAsync($"{key}", cancellationToken);
        }
        public async Task InvalidateCacheAsync(string keyPrefix)
        {
            await _hybrid.RemoveByPrefixAsync($"Safeer:{keyPrefix}");
        }
        #endregion

    }
}
