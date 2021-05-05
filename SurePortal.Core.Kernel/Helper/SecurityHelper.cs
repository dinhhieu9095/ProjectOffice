using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DaiPhatDat.Core.Kernel.Helper
{
    public static class SecurityHelper
    {
        public static bool VerifyCertificatePassword(byte[] certificate, string password)
        {
            try
            {
                var cert = new X509Certificate2(certificate, password);
            }
            catch (CryptographicException ex)
            {
                if ((ex.HResult & 0xFFFF) == 0x56)
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] EncryptFile(byte[] toEncryptArray, string key)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            //of the Cryptographic service provide. Best Practice
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                //set the secret key for the tripleDES algorithm
                Key = keyArray,
                //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                Mode = CipherMode.ECB,
                //padding mode(if any extra byte added)
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            var resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return resultArray;
        }

        public static string EncryptText(string toEncrypt, string key)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var resultArray = EncryptFile(toEncryptArray, key);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static byte[] DecryptFile(byte[] toDecryptArray, string key)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            //of the Cryptographic service provide. Best Practice
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                //set the secret key for the tripleDES algorithm
                Key = keyArray,
                //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                Mode = CipherMode.ECB,
                //padding mode(if any extra byte added)
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock
                    (toDecryptArray, 0, toDecryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            return resultArray;
        }

        public static string DecryptText(string toDecrypt, string key)
        {
            var toDecryptArray = Convert.FromBase64String(toDecrypt);
            var resultArray = DecryptFile(toDecryptArray, key);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
