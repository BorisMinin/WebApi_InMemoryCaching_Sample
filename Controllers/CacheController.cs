using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApi_InMemoryCaching_Sample.Entities;

namespace WebApi_InMemoryCaching_Sample.Controllers
{
    [Route("InMemoryCache/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        // Defining IMemoryCache to access the in-memory cache implementation
        private readonly IMemoryCache memoryCache;

        // Injecting the IMemoryCache to the constructor
        public CacheController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// todo:add comment
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet("{Key}")]
        public IActionResult GetCache(string Key)
        {
            string Value = string.Empty;
            memoryCache.TryGetValue(Key, out Value);

            return Ok(Value);
        }

        /// <summary>
        /// todo:add comment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetCache(CacheRequest data)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5), // the cache will expire guaranteed in minutes
                Priority = CacheItemPriority.High,// default - Normal, can be High, Low and Never Remove
                SlidingExpiration = TimeSpan.FromMinutes(2), // cache expiration in minutes
                Size = 1024, // limit in megabytes
            };
            memoryCache.Set(data.Key, data.Value, cacheExpiryOptions);

            return Ok();
        }
    }
}