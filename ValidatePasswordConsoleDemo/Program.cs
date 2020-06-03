using System;
using System.Security.Cryptography;
using System.Text;

namespace ValidatePasswordConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordValidation();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void PasswordValidation()
        {
        Startup:
            PrintLn("功能选择:\n1) 校验密码\n2) 校验密码强度\nexit)退出 \n输入数字选择功能，回车结束");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ValidatePassword();
                    break;
                case "2":
                    CheckPasswordStrengthLevel();
                    break;
                case "exit":
                    break;
                default:
                    PrintLn("请输入1或2选择功能，输入exit退出");
                    goto Startup;
            }
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

        /// <summary>
        /// 输入密码按照规则进行校验
        /// </summary>
        private static void ValidatePassword()
        {
            try
            {
                PrintLn("请输入用户名,回车结束,输入exit退出过程:");
                var userName = Console.ReadLine();
                if (string.Equals("exit", userName)) return;
                InputPassword:
                PrintLn("请输入要验证的密码,回车结束,输入exit退出过程:");
                var password = Console.ReadLine();
                if (string.Equals("exit", userName)) return;
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
