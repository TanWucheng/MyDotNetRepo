using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ConsoleDemo.PasswordValidation
{
    internal class WeakPwdListReader
    {
        public static string[] Read()
        {
            var path = Directory.GetCurrentDirectory();
            var fileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                $"{path}\\PasswordValidation\\WeakPasswordList.txt" : $"{path}/PasswordValidation/WeakPasswordList.txt";

            var list = new List<string>();
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using var reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    list.Add(reader.ReadLine()?.Trim());
                }
            }
            return list.ToArray();
        }
    }
}