using CacheLayer.Entities;
using StructureMap;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CacheLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheAttribute : StructureMapAttribute
    {
        public const int OneMonth = 2630000;
        public const int OneWeek = 604800;
        public const int OneDay = 86400;
        public const int OneHour = 3600;

        private string _CacheArea;
        private string _CacheName;
        private string _CachePrefix;
        private string _PropertyName;
        private CacheSettingsEnum _CacheSetting = CacheSettingsEnum.Default;

        private static string DefaultCacheName = "default";
        private static string DefaultCacheArea = "default";
        public int TimeoutSeconds = OneDay;

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
                    _CachePrefix = ""; //_CachePrefix = ConfigHelper.Config.Constants.INSTALLATION_IDENTIFIER;

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

        public CacheSettingsEnum CacheSetting
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


        private static readonly List<Type> DisallowedTypes = new List<Type>
        {
                typeof (Stream),

                // typeof (IEnumerable),
                typeof (IQueryable)
        };

        public override void Alter(IConfiguredInstance instance, PropertyInfo property)
        {
            //var value = System.Configuration.ConfigurationManager.AppSettings[_key];

            //instance.Dependencies.AddForProperty(property, "");
        }

        public override void Alter(IConfiguredInstance instance, ParameterInfo parameter)
        {
            //var value = System.Configuration.ConfigurationManager.AppSettings[_key];

            //instance.Dependencies.AddForConstructorParameter(parameter, "");
        }


    }
}
