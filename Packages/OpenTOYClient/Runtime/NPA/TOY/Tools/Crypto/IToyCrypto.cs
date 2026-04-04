namespace NPA.TOY.Tools.Crypto
{
    internal interface IToyCrypto
    {
        byte[] Decrypt(byte[] data);
        byte[] Encrypt(byte[] data);
    }
}