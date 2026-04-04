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
            return ToyCrypto.Decrypt(_key, data);
        }

        public virtual byte[] Encrypt(byte[] data)
        {
            return ToyCrypto.Encrypt(_key, data);
        }
    }
}