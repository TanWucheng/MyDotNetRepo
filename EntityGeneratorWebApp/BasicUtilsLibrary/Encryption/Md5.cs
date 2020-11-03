using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BasicUtilsLibrary.Encryption
{
    public class Md5
    {
        public static string Md5Encrypt(string plainText)
        {
            var buffer = Encoding.Default.GetBytes(plainText); //将字符串解析成字节数组，随便按照哪种解析格式都行
            var md5 = MD5.Create();  //使用MD5这个抽象类的Create()方法创建一个虚拟的MD5类的对象。
            var bufferNew = md5.ComputeHash(buffer); //使用MD5实例的ComputerHash()方法处理字节数组。

            return bufferNew.Aggregate<byte, string>(null, (current, t) => current + t.ToString("x2"));
        }
    }
}
