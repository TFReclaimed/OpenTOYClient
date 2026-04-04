namespace NPA.TOY.Tools.Crypto
{
    internal class ToyAesCryptoNoDecrypt : ToyAesCrypto
    {
        public ToyAesCryptoNoDecrypt(byte[] key) : base(key)
        {
        }

        public override byte[] Decrypt(byte[] data)
        {
            return data;
        }
    }
}