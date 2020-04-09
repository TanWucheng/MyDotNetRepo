using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTimingWebAppDemo.Utils
{
    internal static class ReflectPropertyUtil<T>
    {
        /// <summary>
        /// 反射获得类型属性名称清单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReflectGetPropertyName(T entity)
        {
            var properties = entity.GetType().GetProperties();
            return properties.Select(property => property.Name).ToList();
        }

        /// <summary>
        /// 反射获得类型属性数值清单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReflectGetPropertyValue(T entity)
        {
            var properties = entity.GetType().GetProperties();
            return properties.Select(property => property.GetValue(entity, null)).Select(value => value != null ? value.ToString() : "").ToList();
        }

        /// <summary>
        /// 反射获得类型(属性,属性值)清单,根据给定属性名称清单获取对应属性值键值对
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyList"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ReflectGetPropertyKeyValuePairs(T entity, List<string> propertyList)
        {
            var properties = entity.GetType().GetProperties();
            var dic = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                if (!propertyList.Contains(property.Name)) continue;
                var value = property.GetValue(entity, null);
                dic.Add(property.Name, value != null ? value.ToString() : "");
            }
            return dic;
        }

        /// <summary>
        /// 反射获得类型(属性,属性值)清单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ReflectGetPropertyKeyValuePairs(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var dic = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity, null);
                dic.Add(property.Name, value != null ? value.ToString() : "");
            }
            return dic;
        }

        /// <summary>
        /// 反射获得类型(属性,属性类型代码)清单,根据给定属性名称清单获取对应属性值键值对
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyList"></param>
        /// <returns></returns>
        public static Dictionary<string, TypeCode> ReflectGetPropertyKeyTypePairs(T entity, List<string> propertyList)
        {
            var properties = entity.GetType().GetProperties();
            var dic = new Dictionary<string, TypeCode>();
            foreach (var property in properties)
            {
                if (propertyList.Contains(property.Name))
                {
                    dic.Add(property.Name, Type.GetTypeCode(property.PropertyType));
                }
            }
            return dic;
        }

        /// <summary>
        /// 反射获得类型(属性,属性类型代码)清单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, TypeCode> ReflectGetPropertyKeyTypePairs(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var dic = new Dictionary<string, TypeCode>();
            foreach (var property in properties)
            {
                dic.Add(property.Name, Type.GetTypeCode(property.PropertyType));
            }
            return dic;
        }

        /// <summary>
        /// 反射获得类型属性(名称,数值)字典
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ReflectGetPropertyInfo(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var dic = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity, null);
                dic.Add(property.Name, value != null ? value.ToString() : "");
            }
            return dic;
        }
    }
}