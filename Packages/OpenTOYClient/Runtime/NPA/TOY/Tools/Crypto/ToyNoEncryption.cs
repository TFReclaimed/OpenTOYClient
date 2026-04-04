namespace NPA.TOY.Tools.Crypto
{
    public class ToyNoEncryption : IToyCrypto
    {
        public byte[] Decrypt(byte[] data)
        {
            return data;
        }

        public byte[] Encrypt(byte[] data)
        {
            return data;
        }
    }
}