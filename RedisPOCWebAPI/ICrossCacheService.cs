namespace RedisPOCWebAPI
{
    public interface ICrossCacheService
    {
        Task<T?> GetCacheAsync<T>(string cacheKey,CancellationToken cancellationToken = default);
        Task SetCacheAsync<T>(string cacheKey, T value, CancellationToken cancellationToken = default);
        Task<T> GetOrCreateCacheAsync<T>(string cacheKey,Func<Task<T>> getResonse,CancellationToken cancellationToken = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
        Task InvalidateCacheAsync(string keyPrefix);
    }
}
