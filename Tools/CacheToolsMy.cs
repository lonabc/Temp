using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.Tools
{
    public class CacheToolsMy
    {
        private readonly IMemoryCache _memoryCache;
        public CacheToolsMy(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void  setCachedData(String key,String data)
        {
            _memoryCache.Set(key,data);

        }

        public string GetCachedData(String key)
        {
            if (!_memoryCache.TryGetValue(key, out string cacheData))
            {
                cacheData = GetDataFromSource();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _memoryCache.Set(key,cacheData,cacheEntryOptions);

            }
            return cacheData;
        }
        private string GetDataFromSource()
        {
            // 模拟从数据库或API获取数据
            return DateTime.Now.ToString();
        }
    }
}
