using System;
using System.Reflection;

namespace App.Common.Extensions
{
    public static class GenericExtensions
    {
        public static object SetGenericPropertiesValues(this object destination, object origin)
        {
            foreach (var property in destination.GetType().GetProperties())
            {
                try
                {
                    var prop = origin.GetType().GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.CanWrite)
                        property.SetValue(destination, prop.GetValue(origin), null);
                }
                catch (Exception ex)
                {

                }
            }
            return destination;

        }
    }
}
