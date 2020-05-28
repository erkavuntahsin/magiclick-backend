using System;
using System.Collections.Generic;
using System.Text;

namespace CacheLayer.Entities
{
    public enum CacheSettingsEnum
    {
        Default,
        IgnoreParameters,
        UseID,
        UsePropertyWithCacheName,
        UseParametersWithCacheName,
        UseParametersWithMethodName,
        ReplaceParameterName
    };
}