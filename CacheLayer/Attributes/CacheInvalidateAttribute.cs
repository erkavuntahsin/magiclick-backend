using CacheLayer.Entities;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CacheInvalidateAttribute : StructureMapAttribute
    {
        private string _CacheArea;
        private string _CacheName;
        private string _CachePrefix;
        private string _PropertyName;

        private CacheInvalidateSettingsEnum _CacheSetting = CacheInvalidateSettingsEnum.Default;

        private static string DefaultCacheName = "default";
        private static string DefaultCacheArea = "default";

        public string CacheName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_CacheName))
                    _CacheName = DefaultCacheName;

                if (!string.IsNullOrWhiteSpace(CachePrefix))
                    return CachePrefix + "." + _CacheName;
                else
                    return _CacheName;
            }
            set
            {
                _CacheName = value;
            }
        }

        public string CacheArea
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_CacheArea))
                    _CacheArea = DefaultCacheArea;

                if (!string.IsNullOrWhiteSpace(CachePrefix))
                    return CachePrefix + "." + _CacheArea;
                else
                    return _CacheArea;
            }
            set
            {
                _CacheArea = value;
            }
        }


        public string CachePrefix
        {
            get
            {
                if (_CachePrefix == null)
                    _CachePrefix = "";

                return _CachePrefix;
            }
        }

        public string PropertyName
        {
            get
            {
                if (_PropertyName == null)
                    _PropertyName = string.Empty;

                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
            }
        }

        public CacheInvalidateSettingsEnum CacheSetting
        {
            get
            {
                return _CacheSetting;
            }
            set
            {
                _CacheSetting = value;
            }
        }
    }
}
