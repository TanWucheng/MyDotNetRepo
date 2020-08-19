using System;
using System.Linq;
using ConsoleDemo.PasswordValidation;
using ConsoleDemo.StringComparison;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckPasswordIsWeakly();
        }

        /// <summary>
        /// 输入密码获取密码强度等级
        /// </summary>
        private static void CheckPasswordIsWeakly()
        {
            var arr = WeakPwdListReader.Read();
            PrintLine("请输入用于密码校验的用户名:");
            var username = Console.ReadLine();
        Recycle:
            PrintLine("请输入要校验的密码，回车结束，输入 exit 退出过程:");
            var password = Console.ReadLine();
            if (string.Equals("exit", password)) return;
            PrintLine($"用户名:{username}和密码:{password}的最长交集:{StringSamePart.Get(username, password)}");
            PrintLine($"用户名:{username}和密码:{password}的相似度:{StringSimilarity.Get(username, password)}");
            var result = from p in arr where password.ToLower().IndexOf(p) >= 0 select p;
            PrintLine($"命中的弱密码是: {string.Join(",", result)}");
            var count = result.Count();
            PrintLine(count > 0 ? "密码包含常用字符" : "密码不包含常用字符");
            goto Recycle;
        }

        private static void PrintLine(object o)
        {
            Console.WriteLine(o);
        }
    }
}
