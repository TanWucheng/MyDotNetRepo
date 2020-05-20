using System;

namespace ValidatePasswordConsoleDemo
{
    /// <summary>
    /// 自定义密码验证异常
    /// </summary>
    public class PasswordValidationException : ApplicationException
    {
        public PasswordValidationException(string message) : base(message)
        {

        }
    }
}