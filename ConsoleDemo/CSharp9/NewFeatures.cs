using System;

namespace ConsoleDemo.CSharp9
{
    internal class NewFeatures
    {
        public static void RangeOperation()
        {
            var str = "abcdefg";
            Console.WriteLine($"源字符串:{str}");
            Console.WriteLine($"str[0..^2]:{str[0..^2]}");
        }
    }
}