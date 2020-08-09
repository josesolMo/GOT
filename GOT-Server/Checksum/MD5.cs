using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HuffmanCompres
{
    class MD5
    {
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var service = new TripleDESCryptoServiceProvider())
                {
                    service.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    service.Mode = CipherMode.ECB;
                    service.Padding = PaddingMode.PKCS7;

                    using (var transform = service.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var service = new TripleDESCryptoServiceProvider())
                {
                    service.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    service.Mode = CipherMode.ECB;
                    service.Padding = PaddingMode.PKCS7;

                    using (var transform = service.CreateDecryptor())
                    {
                        byte[] encryptedrBytes = Convert.FromBase64String(encryptedText);
                        byte[] bytes = transform.TransformFinalBlock(encryptedrBytes, 0, encryptedrBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
