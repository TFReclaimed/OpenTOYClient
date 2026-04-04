using System.Security.Cryptography;

namespace NPA.TOY.Tools.Crypto
{
    internal class ToyAesCrypto : IToyCrypto
    {
        private readonly byte[] _key;

        public ToyAesCrypto(byte[] key)
        {
            _key = key;
        }

        public virtual byte[] Decrypt(byte[] data)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.ECB;

            using var decryptor = aes.CreateDecryptor();
            var decryptedData = decryptor.TransformFinalBlock(data, 0, data.Length);
            return decryptedData;
        }

        public virtual byte[] Encrypt(byte[] data)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.ECB;

            using var encryptor = aes.CreateEncryptor();
            var encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            return encryptedData;
        }
    }
}