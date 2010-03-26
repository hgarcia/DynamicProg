using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using LaTrompa.Extensions;

namespace DynamicProg.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns a <c>SortedList</c> with the fields names as the keys and the field values as the values.
        /// </summary>
        public static SortedList<string, object> GetFieldList(this object obj)
        {
            var sl = new SortedList<string, object>();
            var t = obj.GetType();
            var fs = t.GetFields();
            foreach (var fi in fs)
            {
                sl.Add(fi.Name, fi.GetValue(obj));
            }
            return sl;
        }

        /// <summary>
        /// Returns a <c>SortedList</c> with the Property names as the keys and the properties values as the values.
        /// </summary>
        public static SortedList<string,object> GetPropertyList(this object obj)
        {
            var sl = new SortedList<string, object>();
            var t = obj.GetType();
            var ps = t.GetProperties();
            foreach (var pi in ps)
            {
                sl.Add(pi.Name, pi.GetValue(obj, null));
            }
            return sl;
        }

        public static SortedList<string, object> GetFieldAndPropertyList(this object obj)
        {
            var sl = GetFieldList(obj);
            return (SortedList<string,object>)sl.Combine(GetPropertyList(obj));
        }

        public static T SetProperty<T>(this T obj, string propertyName, object value)
        {
            var t = obj.GetType();

            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;

            var propertyInfo = t.GetProperty(propertyName, flags);
            var propType = propertyInfo.PropertyType;

            if (value != null)
            {
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    var nullableConverter = new NullableConverter(propType);
                    propType = nullableConverter.UnderlyingType;
                }
                value = Convert.ChangeType(value, propType);
            }

            propertyInfo.SetValue(obj, value, null);
            return obj;
        }

        public static T SetField<T>(this T obj, string fieldName, object value)
        {
            var t = obj.GetType();

            var fieldInfo = t.GetField(fieldName);
            var fieldType = fieldInfo.FieldType;

            if (value != null)
            {
                if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    var nullableConverter = new NullableConverter(fieldType);
                    fieldType = nullableConverter.UnderlyingType;
                }
                value = Convert.ChangeType(value, fieldType);
            }

            fieldInfo.SetValue(obj, value);
            return obj;
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return  (T)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static T GetFieldValue<T>(this object obj, string fieldName)
        {
            return (T) obj.GetType().GetField(fieldName).GetValue(obj);
        }

        public static T Hydrate<T>(this T obj, NameValueCollection values)
        {
            foreach (var key in values.AllKeys)
            {
                try
                {
                    obj.SetProperty(key, values[key]);
                }
                catch (Exception){continue;}         
            }

            return obj;
        } 
    }
}
