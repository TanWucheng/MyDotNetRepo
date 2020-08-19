using System.Windows;
using System.Windows.Media;

namespace DbTableToDotnetEntity.Domain
{
    internal class VisualTreeFinder
    {
        /// <summary>
        /// 查找子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T dependencyObject)
                    return dependencyObject;
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        /// <summary>
        /// 查找子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is T element && (element.Name == name | string.IsNullOrEmpty(name)))
                {
                    return element;
                }

                var grandChild = FindVisualChild<T>(child, name);
                if (grandChild != null)
                    return grandChild;
            }
            return null;
        }
    }
}
