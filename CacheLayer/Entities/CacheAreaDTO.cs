using System;
using System.Collections.Generic;
using System.Text;

namespace CacheLayer.Entities
{
    public class CacheAreaDTO
    {
        public string _CacheAreaName { get; set; }

        public string _CacheKey { get; set; }

        public CacheAreaDTO(string CacheAreaName, string CacheKey)
        {
            _CacheAreaName = CacheAreaName;
            _CacheKey = CacheKey;
        }
    }
}
