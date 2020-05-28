using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using StructureMap.DynamicInterception;

namespace CacheLayer.Entities
{
    public class ObjectPropertyReflectHelper
    {
        internal object GetValue(IArgument o, String propertyName)
        {
            var properties = TypeDescriptor.GetProperties(o.Value);
            var property = GetPropertyCached(propertyName, properties, o.GetType());
            if (property == null)
            {
                return null;
            }

            return property.GetValue(o.Value);
        }

        private static PropertyDescriptor GetPropertyCached(string name, PropertyDescriptorCollection properties, Type t)
        {
            if (PropertyCache[t] == null)
            {
                PropertyCache[t] = new Hashtable();
            }
            if (((Hashtable)PropertyCache[t])[name] == null)
            {
                ((Hashtable)PropertyCache[t])[name] = GetProperty(name, properties, t);
            }
            return ((Hashtable)PropertyCache[t])[name] as PropertyDescriptor;
        }

        //is threadsafe?
        private readonly static Hashtable PropertyCache = new Hashtable();

        private static PropertyDescriptor GetProperty(string name, PropertyDescriptorCollection properties, Type t)
        {
            var property = properties.Find(name, true);
            if (property != null)
            {
                return property;
            }
            foreach (PropertyInfo prop in t.GetProperties())
            {
                property = properties[prop.Name];
                if (property != null)
                {
                    return property;
                }
            }
            return property;
        }
    }
}