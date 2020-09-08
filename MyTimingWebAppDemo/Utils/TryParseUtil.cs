using System;

namespace MyTimingWebAppDemo.Utils
{
    /// <summary>
    /// 类型转换工具
    /// </summary>
    internal static class TryParseUtil
    {
        /// <summary>
        /// 转换为int32
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int TryParseInt32(string origin, int defaultValue)
        {
            return int.TryParse(origin, out var outValue) ? outValue : defaultValue;
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal TryParseDecimal(string origin, decimal defaultValue)
        {
            return decimal.TryParse(origin, out var outValue) ? outValue : defaultValue;
        }

        /// <summary>
        /// 转换为datetime
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <returns></returns>
        public static DateTime? TryParseDateTime(string origin)
        {
            if (DateTime.TryParse(origin, out var outValue))
            {
                return outValue;
            }

            return null;
        }

        /// <summary>
        /// 转换为boolen,0:false,1:true,其他值使用系统内置转换方法
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static bool TryParseBoolean(string origin)
        {
            if (origin == "1")
            {
                return true;
            }
            else if (origin == "0")
            {
                return false;
            }
            else
            {
                return bool.TryParse(origin, out var b) && b;
            }
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long TryParseInt64(string origin, long defaultValue)
        {
            return long.TryParse(origin, out var l) ? l : defaultValue;
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double TraParseDouble(string origin, double defaultValue)
        {
            return double.TryParse(origin, out var d) ? d : defaultValue;
        }
    }
}