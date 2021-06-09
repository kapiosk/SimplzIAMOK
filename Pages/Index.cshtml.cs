using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace SimplzIAMOK.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;

        public IndexModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {

        }
    }
}
