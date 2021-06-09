using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplzIAMOK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Areyouokcontroller : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public Areyouokcontroller(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{whoami}")]
        public async Task<IActionResult> GetPersons([FromRoute] string whoami)
        {
            try
            {
                var hs = await GetOrCreateAsync(whoami, async () =>
                {
                    await GetOrCreateAsync("keys", async () => await Task.FromResult(new List<string>() { whoami }));
                    return await Task.FromResult(new HashSet<DateTime>());
                });
                hs.Add(DateTime.Now);
            }
            catch
            { }
            return Ok();
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        {
            return await _memoryCache.GetOrCreateAsync(key, cacheEntry =>
            {
                return factory(); ;
            });
        }
    }
}
