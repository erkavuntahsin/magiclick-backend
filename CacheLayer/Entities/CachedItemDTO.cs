using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CacheLayer.Entities
{
    public enum CacheStatus
    {
        Ready = 0,
        Refreshing = 1,
    }
    
    public class CachedItemDTO
    {
        public DateTime SetTime { get; set; }

        public DateTime ExpirationTime { get; set; }

        public double TimeoutSeconds { get; set; }

        public object CacheValue { get; set; }

        public object[] MethodParameters { get; set; }

        public MethodBase MethodBase { get; set; }

        public CacheStatus Status { get; set; }
    }
}
