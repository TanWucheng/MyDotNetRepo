using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace DbTableToDotnetEntity.Domain
{
    /// <summary>
    /// 验证行为类,可以获得附加到的对象
    /// </summary>
    public class ValidationExceptionBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 错误计数器
        /// </summary>
        private int _validationExceptionCount = 0;

        /// <summary>
        /// 附加对象时，给对象增加一个监听验证错误事件的能力，注意该事件是冒泡的
        /// </summary>
        protected override void OnAttached()
        {
            this.AssociatedObject.AddHandler(Validation.ErrorEvent, new EventHandler<ValidationErrorEventArgs>(OnValidationError));
        }

        /// <summary>
        /// 获取实现接口的对象
        /// </summary>
        /// <returns></returns>
        private IValidationExceptionHandler GetValidationExceptionHandler()
        {
            var handler = AssociatedObject.DataContext as IValidationExceptionHandler;
            return handler;
        }

        /// <summary>
        /// 验证事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            var handler = GetValidationExceptionHandler();

            if (handler == null || !(e.OriginalSource is UIElement element))
            {
                return;
            }

            if (e.Action == ValidationErrorEventAction.Added)
            {
                _validationExceptionCount++;
            }
            else if (e.Action == ValidationErrorEventAction.Removed)
            {
                _validationExceptionCount--;
            }

            handler.IsValid = _validationExceptionCount == 0;
        }
    }
}
