using System;
using System.Collections.Generic;

namespace ValidatePasswordConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var level = ValidatePassword.GetPasswordLevel("^$#@!aB1%*heUijkR&85@)^#fTW*&");
            //PrintLn(level);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void ValidatePassword()
        {
            try
            {
                PrintLn("请输入要验证的密码,回车结束:");
                var password = Console.ReadLine();
                

            }
            catch (System.Exception ex)
            {
                PrintLn(ex.Message);
            }
        }

        private static void PrintLn(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
