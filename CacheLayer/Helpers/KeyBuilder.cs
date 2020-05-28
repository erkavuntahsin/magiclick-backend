using StructureMap.DynamicInterception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CacheLayer.Entities
{
    public static class KeyBuilder
    {
        #region Build Methods

        internal static string BuildCacheKey(CacheSettingsEnum settingType, string KeyString, string InstanceName, string MethodName, string PropertyName = null, IArgument[] args = null)
        {
            string key = "";
            switch (settingType)
            {

                case CacheSettingsEnum.IgnoreParameters:
                    key = GenerateKey(InstanceName, MethodName);
                    break;

                case CacheSettingsEnum.UseID:
                    key = GenerateKey(KeyString, args, "id");
                    break;

                case CacheSettingsEnum.UseParametersWithMethodName:
                    key = GenerateKey(InstanceName, MethodName, args);
                    break;

                case CacheSettingsEnum.UseParametersWithCacheName:
                    key = GenerateKey(KeyString, args);
                    break;

                case CacheSettingsEnum.UsePropertyWithCacheName:
                    key = GenerateKey(KeyString, args, PropertyName);
                    break;

                case CacheSettingsEnum.ReplaceParameterName:
                    key = KeyString;
                    break;

                case CacheSettingsEnum.Default:
                    key = KeyString;
                    break;
            }

            return key;
        }

        internal static string BuildInvalidateCacheKey(CacheInvalidateSettingsEnum settingType, string CacheName, string PropertyName = null, IArgument[] args = null)
        {
            string key = "";
            switch (settingType)
            {
                case CacheInvalidateSettingsEnum.UseParameters:
                    key = GenerateKey(CacheName, args);
                    break;

                case CacheInvalidateSettingsEnum.UsePropertyWithCacheName:
                    key = GenerateKey(CacheName, args, PropertyName);
                    break;

                case CacheInvalidateSettingsEnum.Default:
                    key = CacheName;
                    break;
            }

            return key;
        }

        #endregion

        private static string GenerateKey(string Key, IArgument[] args, string propertyName)
        {
            StringBuilder keyBuilder = new StringBuilder();

            keyBuilder.Append(Key);
            keyBuilder.Append("_");

            if (args != null)
            {
                try
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        string[] propertyNamePart = propertyName.Split('.');
                        if (args[i].InstanceParameterInfo.Name == propertyNamePart[0])
                        {
                            if (propertyNamePart.Length == 1)
                            {
                                keyBuilder.Append(args[i].Value);
                            }
                            else if (propertyNamePart.Length == 2)
                            {
                                var val = new ObjectPropertyReflectHelper().GetValue(args[i], propertyNamePart[1]);

                                if (val == null)
                                {
                                    continue;
                                }
                                keyBuilder.Append(val);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return keyBuilder.ToString();
        }

        private static string GenerateKey(string InstanceName, string MethodName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(InstanceName);
            sb.Append(".");
            sb.Append(MethodName);

            return sb.ToString();
        }

        private static string GenerateKey(string InstanceName, string MethodName, IArgument[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(InstanceName);
            sb.Append(".");
            sb.Append(MethodName);
            foreach (var argument in args)
            {
                sb = AddKeyPart(argument, sb);
            }

            return sb.ToString();
        }

        private static string GenerateKey(string Key, IArgument[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Key);
            foreach (var argument in args)
            {
                sb = AddKeyPart(argument, sb);
            }

            return sb.ToString();
        }

        private static StringBuilder AddKeyPart(IArgument argument, StringBuilder sb)
        {
            sb.Append("_");
            try
            {
                sb.Append(argument.Value);
            }
            catch (Exception)
            {
                sb.Append(argument.GetHashCode());
            }

            return sb;
        }
    }
}