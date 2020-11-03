using System;
using System.Runtime.InteropServices;

namespace BasicUtilsLibrary.Encryption
{
    public static class Rsa
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintText">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="encryptedText">密文</param>
        /// <returns></returns>
        public static bool Encrypt(string plaintText, string publicKey, out string encryptedText)
        {
            try
            {
                encryptedText = RsaHelperForBrowser.Encrypt(publicKey, plaintText);
                return true;
            }
            catch (Exception)
            {
                encryptedText = plaintText;
                return false;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintText">明文</param>
        /// <param name="encryptedText">密文</param>
        /// <returns></returns>
        public static bool Encrypt(string plaintText, out string encryptedText)
        {
            try
            {
                var path = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/dotnetcore/rsakeys/RSA.Pub" : "C:\\ProgramData\\MyRSAKeys\\RSA.Pub";
                encryptedText = RsaHelperForGui.Encrypt(plaintText, path);
                return true;
            }
            catch (Exception)
            {
                encryptedText = plaintText;
                return false;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="decryptedText">明文</param>
        /// <returns></returns>
        public static bool Decrypt(string encryptedText, string privateKey, out string decryptedText)
        {
            try
            {
                decryptedText = RsaHelperForBrowser.Decrypt(privateKey, encryptedText);
                return true;
            }
            catch (Exception)
            {
                decryptedText = encryptedText;
                return false;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="decryptedText">明文</param>
        /// <returns></returns>
        public static bool Decrypt(string encryptedText, out string decryptedText)
        {
            try
            {
                var path = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/dotnetcore/rsakeys/RSA.Private" : "C:\\ProgramData\\MyRSAKeys\\RSA.Private";
                decryptedText = RsaHelperForGui.Decrypt(encryptedText, path);
                return true;
            }
            catch (Exception)
            {
                decryptedText = encryptedText;
                return false;
            }
        }
    }
}