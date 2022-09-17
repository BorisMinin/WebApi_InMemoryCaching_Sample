using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApi_InMemoryCaching_Sample.Data;
using WebApi_InMemoryCaching_Sample.Entities;

namespace WebApi_InMemoryCaching_Sample.Controllers
{
    [Route("InMemoryCache/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMemoryCache memoryCache; // InMemoryCache definition
        private readonly ApplicationDbContext context; // ApplicationDbContext definition
        public CustomerController(IMemoryCache memoryCache, ApplicationDbContext context)
        {
            this.memoryCache = memoryCache;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "customerList";
            if (!memoryCache.TryGetValue(cacheKey, out List<Customer> customerList))
            {
                customerList = await context.Customers.ToListAsync();
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                memoryCache.Set(cacheKey, customerList, cacheExpiryOptions);
            }
            return Ok(customerList);
        }
    }
}