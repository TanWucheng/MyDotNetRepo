using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace EncryptionLibrary
{
    internal static class RsaHelperForBrowser
    {
        /// <summary>
        /// 生成PEM格式的公钥和密钥
        /// </summary>
        /// <param name="strength">长度</param>
        /// <returns>(PublicKey,PrivateKey)</returns>
        public static (string, string) CreateKeyPair(int strength = 1024)
        {
            var r = new RsaKeyPairGenerator();
            r.Init(new KeyGenerationParameters(new SecureRandom(), strength));
            var keys = r.GenerateKeyPair();

            TextWriter privateTextWriter = new StringWriter();
            var privatePemWriter = new PemWriter(privateTextWriter);
            privatePemWriter.WriteObject(keys.Private);
            privatePemWriter.Writer.Flush();

            TextWriter publicTextWriter = new StringWriter();
            var publicPemWriter = new PemWriter(publicTextWriter);
            publicPemWriter.WriteObject(keys.Public);
            publicPemWriter.Writer.Flush();

            return (publicTextWriter.ToString(), privateTextWriter.ToString());
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="plainText">待加密的字符串</param>
        /// <returns>加密后的Base64</returns>
        public static string Encrypt(string publicKey, string plainText)
        {
            using TextReader reader = new StringReader(publicKey);
            var key = new PemReader(reader).ReadObject() as AsymmetricKeyParameter;
            var rsaEncrypt = new Pkcs1Encoding(new RsaEngine());
            rsaEncrypt.Init(true, key);//加密是true；解密是false;
            var entData = Encoding.UTF8.GetBytes(plainText);
            entData = rsaEncrypt.ProcessBlock(entData, 0, entData.Length);
            return Convert.ToBase64String(entData);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="encryptedText">待解密的字符串(Base64)</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string privateKey, string encryptedText)
        {
            using TextReader reader = new StringReader(privateKey);
            dynamic key = new PemReader(reader).ReadObject();
            var rsaDecrypt = new Pkcs1Encoding(new RsaEngine());
            key = key switch
            {
                AsymmetricKeyParameter parameter => parameter,
                AsymmetricCipherKeyPair pair => pair.Private,
                _ => key
            };
            rsaDecrypt.Init(false, key);  //这里加密是true；解密是false  

            var entData = Convert.FromBase64String(encryptedText);
            entData = rsaDecrypt.ProcessBlock(entData, 0, entData.Length);
            return Encoding.UTF8.GetString(entData);
        }
    }
}