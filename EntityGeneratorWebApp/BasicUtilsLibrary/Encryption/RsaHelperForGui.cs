using System;
using System.IO;
using System.Security.Cryptography;

namespace BasicUtilsLibrary.Encryption
{
    internal static class RsaHelperForGui
    {
        /// <summary>
        /// 用给定路径的RSA公钥文件加密纯文本。
        /// </summary>
        /// <param name="plainText">要加密的文本</param>
        /// <param name="pathToPublicKey">用于加密的公钥路径.</param>
        /// <returns>表示加密数据的64位编码字符串.</returns>
        public static string Encrypt(string plainText, string pathToPublicKey)
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                //加载公钥
                var publicXmlKey = File.ReadAllText(pathToPublicKey);
                rsa.FromXmlString(publicXmlKey);
                var bytesToEncrypt = System.Text.Encoding.Unicode.GetBytes(plainText);
                var bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);
                return Convert.ToBase64String(bytesEncrypted);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        /// <summary>
        /// 用给定的RSA公钥加密纯文本
        /// </summary>
        /// <param name="plainText">要加密的文本</param>
        /// <param name="publicKey">RSA公钥</param>
        /// <param name="encryptedText">已加密的文本</param>
        public static void Encrypt(string plainText, string publicKey, out string encryptedText)
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                rsa.FromXmlString(publicKey);
                var bytesToEncrypt = System.Text.Encoding.Unicode.GetBytes(plainText);
                var bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);
                encryptedText = Convert.ToBase64String(bytesEncrypted);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        /// <summary>
        /// 用给定路径的RSA私钥文件解密加密文本
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="pathToPrivateKey">用于加密的私钥路径.</param>
        /// <returns>未加密数据的字符串</returns>
        public static string Decrypt(string encryptedText, string pathToPrivateKey)
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                var privateXmlKey = File.ReadAllText(pathToPrivateKey);
                //rsa.FromXmlString(privateXmlKey);
                RsaExtension.FromXmlString(rsa, privateXmlKey);
                var bytesEncrypted = Convert.FromBase64String(encryptedText);
                var bytesPlainText = rsa.Decrypt(bytesEncrypted, false);
                return System.Text.Encoding.Unicode.GetString(bytesPlainText);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        /// <summary>
        /// 用给定的RSA私钥解密加密文本
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="privateKey">RSA私钥</param>
        /// <param name="plainText">解密好的明文</param>
        public static void Decrypt(string encryptedText, string privateKey, out string plainText)
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                rsa.FromXmlString(privateKey);
                var bytesEncrypted = Convert.FromBase64String(encryptedText);
                var bytesPlainText = rsa.Decrypt(bytesEncrypted, false);
                plainText = System.Text.Encoding.Unicode.GetString(bytesPlainText);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }
}