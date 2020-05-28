using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;


namespace CacheLayer.Entities
{
    public class CacheManager
    {
        private static object _syncObj = new object();
        private static MemoryCache _cacheInstance = null;

        private static List<CacheAreaDTO> CacheAreaList = new List<CacheAreaDTO>();

        private CacheManager()
        {
            
        }

        private static MemoryCache CacheInstance
        {
            get
            {
                if (_cacheInstance == null)
                {
                    _cacheInstance = new MemoryCache(new MemoryCacheOptions());
                }

                return _cacheInstance;
            }
        }
        
        public static void AddValue(string area, string key, object obj, int timeoutSeconds = 2630000)
        {
            try
            {
                if (obj == null)
                {
                    return;
                }

                var cachedItem = new CachedItemDTO()
                {
                    SetTime = DateTime.UtcNow,
                    ExpirationTime = DateTime.UtcNow + TimeSpan.FromSeconds(timeoutSeconds),
                    TimeoutSeconds = timeoutSeconds,
                    CacheValue = obj,
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                        .SetSlidingExpiration(TimeSpan.FromSeconds(timeoutSeconds))
                                        .SetPriority(CacheItemPriority.High)
                                        .RegisterPostEvictionCallback(
                (echoKey, value, reason, substate) =>
                {
                    // Triggered when this cache item is evicted.
                    // Add Log
                });

                CacheInstance.Set(key, cachedItem, cacheEntryOptions);

                AddCacheArea(new CacheAreaDTO(area,key));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static CachedItemDTO GetValue(string area, string key)
        {
            CachedItemDTO cacheItem;
            bool resultStatus = CacheInstance.TryGetValue(key, out cacheItem);
            if (resultStatus)
            {
                if (!CacheAreaList.Any(ca => ca._CacheAreaName == area && ca._CacheKey == key))
                    CacheAreaList.Add(new CacheAreaDTO(area, key));

                return cacheItem;
            }
            else
                return null;
        }

        public static T GetValue<T>(string key) where T : class, new()
        {
            CachedItemDTO cacheItem;
            bool resultStatus = CacheInstance.TryGetValue(key, out cacheItem);
            if (resultStatus)
            {
                try
                {
                    return (T)cacheItem.CacheValue;
                }
                catch (Exception)
                {
                    CacheInstance.Remove(key);
                    throw;
                }
            }
            else
                return null;
        }

        private static IEnumerable<CacheAreaDTO> GetCacheArea(Func<CacheAreaDTO, bool> predicate) => CacheAreaList.Where(predicate);

        public static void RemoveValue(string key)
        {
            try
            {
                CacheInstance.Remove(key);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void RemoveCacheArea(CacheAreaDTO area)
        {
            CacheAreaList.Remove(area);
        }

        public static void RemoveCacheArea(Func<CacheAreaDTO, bool> predicate)
        {
            List<CacheAreaDTO> currentCacheKeyList = new List<CacheAreaDTO>();

            currentCacheKeyList =  GetCacheArea(predicate).ToList();

            if (currentCacheKeyList.Count > 0)
            {
                foreach (var item in currentCacheKeyList)
                {
                    RemoveValue(item._CacheKey);
                    RemoveCacheArea(item);
                }
            }
        }

        public static void AddCacheArea(CacheAreaDTO area)
        {
            if (CacheAreaList == null)
                CacheAreaList = new List<CacheAreaDTO>();

            if (!CacheAreaList.Any(ca => ca._CacheKey == area._CacheKey && ca._CacheAreaName == area._CacheAreaName))
                CacheAreaList.Add(area);
        }

    }
}
