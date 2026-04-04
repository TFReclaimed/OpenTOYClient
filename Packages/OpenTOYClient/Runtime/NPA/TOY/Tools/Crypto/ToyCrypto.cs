using System.Security.Cryptography;
using System.Text;

namespace NPA.TOY.Tools.Crypto
{
    internal static class ToyCrypto
    {
        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = CipherMode.ECB;

            using var decryptor = aes.CreateDecryptor();
            var decryptedData = decryptor.TransformFinalBlock(data, 0, data.Length);
            return decryptedData;
        }

        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = CipherMode.ECB;

            using var encryptor = aes.CreateEncryptor();
            var encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            return encryptedData;
        }

        public static byte[] Encrypt(string key, byte[] data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            return Encrypt(keyBytes, data);
        }
    }
}