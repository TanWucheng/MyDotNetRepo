using System;
using System.Globalization;
using System.Windows.Data;

namespace DbTableToDotnetEntity.Domain
{
    /// <summary>
    /// 取布尔类型相反值的控件属性值转换器
    /// </summary>
    internal class NegateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                return !v;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                return !v;
            }

            return value;
        }
    }
}
