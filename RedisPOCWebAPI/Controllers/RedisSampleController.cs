using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace RedisPOCWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisSampleController(IHybridCachingProvider provider) : ControllerBase
    {
        //private const string servicesListCacheKey = "servicesList";

        
        //private static readonly SemaphoreSlim semaphore = new(1, 1); 

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //Set
           //await provider. SetAsync("xx", "123", TimeSpan.FromMinutes(5));

            var xx= await provider.GetAsync<string>("xx");
            //if (_distributedCache.TryGetValue(servicesListCacheKey, out List<SafeerServices> services))
            //{
            //    //Employee list found in cache
            //}
            //else
            //{  // Employee list not found in cache. Fetching from database.
            //    try
            //    {
            //        await semaphore.WaitAsync();

            //        // Fetching Data from db 
            //        services = getServicesListFromDB();

            //        var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60)).SetAbsoluteExpiration(TimeSpan.FromMinutes(120));
            //        await _distributedCache.SetAsync(servicesListCacheKey, services, cacheEntryOptions);
            //    }
            //    finally
            //    {
            //        semaphore.Release();
            //    }
            //}

            return Ok(xx);
        }


        private List<SafeerServices> getServicesListFromDB() => new()
        {
                new SafeerServices() {
                    Id = 1,
                    NameAr="test1",
                    NameEn=""
                },
                new SafeerServices() {
                    Id = 2,
                    NameAr="test2",
                    NameEn="test2"
                }

        };
    }
}
