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

        [HttpGet("{Key}")]
        public IActionResult GetCache(string Key)
        {
            string Value = string.Empty;
            memoryCache.TryGetValue(Key, out Value);

            return Ok(Value);
        }

        [HttpPost]
        public IActionResult SetCache(CacheRequest data)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
            memoryCache.Set(data.Key, data.Value, cacheExpiryOptions);
            
            return Ok();
        }

    }
}