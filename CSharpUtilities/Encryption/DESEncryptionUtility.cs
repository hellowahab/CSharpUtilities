using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSharpUtilities.Encryption
{
    class DESEncryptionUtility
    {
        public static string EncryptUsingDes(string password, string secretKey, bool hashingEnabled)
        {
            const string key = "Your default key";
            if (string.IsNullOrEmpty(secretKey))
                secretKey = key;
            byte[] keyArray;
            byte[] contentArray = UTF8Encoding.UTF8.GetBytes(password);

            if (hashingEnabled)
            {
                MD5CryptoServiceProvider oMd5CryptoServiceProvider = new MD5CryptoServiceProvider(); ;
                keyArray = oMd5CryptoServiceProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(secretKey));
                oMd5CryptoServiceProvider.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(secretKey);
            }

            TripleDESCryptoServiceProvider oTripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            oTripleDesCryptoServiceProvider.Key = keyArray;
            oTripleDesCryptoServiceProvider.Mode = CipherMode.ECB;
            oTripleDesCryptoServiceProvider.Padding = PaddingMode.PKCS7;

            ICryptoTransform cryptoTransform = oTripleDesCryptoServiceProvider.CreateEncryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(contentArray, 0, contentArray.Length);
            oTripleDesCryptoServiceProvider.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }
    }
}
