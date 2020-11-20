using System;
using System.Security.Cryptography;
using System.Xml;

namespace EncryptionLibrary
{
    internal static class RsaExtension
    {

        public static void FromXmlString(this RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            if (xmlDoc.DocumentElement != null && xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
        {
            var parameters = rsa.ExportParameters(includePrivateParameters);

            return includePrivateParameters ?
                $"<RSAKeyValue><Modulus>{Convert.ToBase64String(parameters.Modulus)}</Modulus><Exponent>{Convert.ToBase64String(parameters.Exponent)}</Exponent><P>{Convert.ToBase64String(parameters.P)}</P><Q>{Convert.ToBase64String(parameters.Q)}</Q><DP>{Convert.ToBase64String(parameters.DP)}</DP><DQ>{Convert.ToBase64String(parameters.DQ)}</DQ><InverseQ>{Convert.ToBase64String(parameters.InverseQ)}</InverseQ><D>{Convert.ToBase64String(parameters.D)}</D></RSAKeyValue>"
                :
                $"<RSAKeyValue><Modulus>{Convert.ToBase64String(parameters.Modulus)}</Modulus><Exponent>{Convert.ToBase64String(parameters.Exponent)}</Exponent></RSAKeyValue>";
        }

    }
}