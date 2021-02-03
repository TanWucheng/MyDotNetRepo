using System;
using RsaUtils;

namespace Encrypt
{
    public class Program
    {
        private static string _jsPrivateKey = string.Empty;
        private static string _jsPublicKey = string.Empty;
        private static string _netPrivateKey = string.Empty;
        private static string _netPublicKey = string.Empty;

        private static void Main(string[] args)
        {
        选择:
            Console.WriteLine("请选择功能:");
            Console.WriteLine("1.获取BouncyCastle工具生成的RSA公钥私钥;");
            Console.WriteLine("2.获取Cryptography(Microsoft)工具生成的RSA公钥私钥;");
            Console.WriteLine("3.BouncyCastle工具公钥加密;");
            Console.WriteLine("4.Cryptography(Microsoft)工具公钥加密;");
            Console.WriteLine("5.输入exit退出程序;");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        try
                        {
                            (_jsPublicKey, _jsPrivateKey) = JsRsaHelper.GenerateKeyPair();
                            Console.WriteLine("BouncyCastle公钥:");
                            Console.WriteLine(_jsPublicKey);
                            Console.WriteLine("BouncyCastle私钥:");
                            Console.WriteLine(_jsPrivateKey);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("生成BouncyCastle密钥发生异常:");
                            Console.WriteLine(e.Message);
                        }
                        goto 选择;
                    }
                case "2":
                    {
                        try
                        {
                            (_netPublicKey, _netPrivateKey) = NetRsaHelper.GenerateKeyPair();
                            Console.WriteLine("Cryptography(Microsoft)公钥:");
                            Console.WriteLine(_netPublicKey);
                            Console.WriteLine("Cryptography(Microsoft)私钥:");
                            Console.WriteLine(_netPrivateKey);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("生成Cryptography(Microsoft)密钥发生异常:");
                            Console.WriteLine(e.Message);
                        }
                        goto 选择;
                    }
                case "3":
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(_jsPublicKey))
                            {
                                Console.WriteLine("请先生成BouncyCastle公钥!");
                                goto 选择;
                            }

                        BouncyCastle重做:
                            Console.WriteLine("请输入要BouncyCastle加密的文本:");
                            var plainText = Console.ReadLine();
                            var encryptedText = JsRsaHelper.Encrypt(_jsPublicKey, plainText);
                            Console.WriteLine("公钥:");
                            Console.WriteLine(_jsPublicKey);
                            Console.WriteLine($"[{plainText}]已加密为:");
                            Console.WriteLine(encryptedText);
                            Console.WriteLine("请问要继续输入加密文本吗?Y/N");
                            var yesOrNot = Console.ReadLine();
                            if (yesOrNot != null && yesOrNot.ToLower() == "y")
                            {
                                goto BouncyCastle重做;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("BouncyCastle加密发生异常:");
                            Console.WriteLine(e.Message);
                        }

                        goto 选择;
                    }
                case "4":
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(_netPublicKey))
                            {
                                Console.WriteLine("请先生成Cryptography(Microsoft)公钥!");
                                goto 选择;
                            }

                        Cryptography重做:
                            Console.WriteLine("请输入要Cryptography(Microsoft)加密的文本:");
                            var plainText = Console.ReadLine();
                            NetRsaHelper.Encrypt(plainText, _netPublicKey, out var encryptedText);
                            Console.WriteLine("公钥:");
                            Console.WriteLine(_netPublicKey);
                            Console.WriteLine($"[{plainText}]已加密为:");
                            Console.WriteLine(encryptedText);
                            Console.WriteLine("请问要继续输入加密文本吗?Y/N");
                            var yesOrNot = Console.ReadLine();
                            if (yesOrNot != null && yesOrNot.ToLower() == "y")
                            {
                                goto Cryptography重做;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Cryptography(Microsoft)加密发生异常:");
                            Console.WriteLine(e.Message);
                        }

                        goto 选择;
                    }
                case "exit":
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("输入有误!请重新选择");
                        goto 选择;
                    }
            }
        }
    }
}
