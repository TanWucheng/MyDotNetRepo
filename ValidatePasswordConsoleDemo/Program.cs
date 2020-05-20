using System;

namespace ValidatePasswordConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ValidatePassword();
            CheckPasswordStrengthLevel();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// 输入密码获取密码强度等级
        /// </summary>
        private static void CheckPasswordStrengthLevel()
        {
        Recycle:
            PrintLn("请输入要获取强度等级的密码,回车结束,输入exit退出过程:");
            var password = Console.ReadLine();
            if (string.Equals("exit", password)) return;
            var level = PasswordValidator.GetPasswordLevel(password);
            PrintLn($"密码强度等级:{level}");
            goto Recycle;
        }

        private static void ValidatePassword()
        {
            try
            {
                PrintLn("请输入用户名,回车结束:");
                var userName = Console.ReadLine();
            InputPassword:
                PrintLn("请输入要验证的密码,回车结束:");
                var password = Console.ReadLine();
                var result = DoValidation(userName, password);
                if (result)
                {
                    PrintLn("验证通过");
                    return;
                }
                else
                {
                    goto InputPassword;
                }
            }
            catch (Exception ex)
            {
                PrintLn(ex.Message);
            }
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool DoValidation(string userName, string password)
        {
            try
            {
                var result = PasswordValidator.CheckIsPasswordValid(userName, password);
                return result;
            }
            catch (PasswordValidationException ex)
            {
                PrintLn("请检查一下验证未通过信息:");
                PrintLn(ex.Message);
                return false;
            }
        }

        private static void PrintLn(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
