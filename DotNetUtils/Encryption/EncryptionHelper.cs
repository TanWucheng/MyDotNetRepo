using System;
using System.Security.Cryptography;
using System.Text;

namespace DotNetUtils.Encryption
{
    public static class EncryptionHelper
    {
        /// <summary>
        /// MD5 加密字符串（32 位大写）
        /// </summary>
        /// <param name="source"> 源字符串 </param>
        /// <returns> 加密后的字符串 </returns>
        public static string Md5(string source)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(source);
            var result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }
    }
}
