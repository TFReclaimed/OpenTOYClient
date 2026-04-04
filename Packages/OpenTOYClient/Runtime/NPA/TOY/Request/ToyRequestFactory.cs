using System;
using System.Globalization;
using System.Text;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal static class ToyRequestFactory
    {
        private const string CommonAesKey = "dd4763541be100910b568ca6d48268e3";

        public static ToyInfoObject ToyInfo { get; set; }

        public static AbstractToyRequest CreateRequest(ToyRequestType requestType, ToySession session)
        {
            var crypto = MakeCrypto(requestType, session);

            switch (requestType)
            {
                /*case ToyRequestType.GetUserInfo:
                    return new ToyGetUserInfoRequest(session, crypto);

                case ToyRequestType.CheckEmailAccountRegistered:
                    return new ToyCheckEmailAccountRegisteredRequest(session, crypto);

                case ToyRequestType.EmailAccountSignUp:
                    return new ToyEmailAccountSignUpRequest(session, crypto);

                case ToyRequestType.EmailAccountResetPassword:
                    return new ToyEmailAccountResetPasswordRequest(session, crypto);

                case ToyRequestType.GetEmailUserInfo:
                    return new ToyGetEmailUserInfoRequest(session, crypto);*/

                case ToyRequestType.EnterToy:
                    return new ToyEnterRequest(session, crypto);

                /*case ToyRequestType.LoginWithEmail:
                case ToyRequestType.LoginWithGuest:
                    return new ToyLoginRequest(session, crypto);*/

                default:
                    throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
        }

        private static IToyCrypto MakeCrypto(ToyRequestType requestType, ToySession session)
        {
            switch (requestType)
            {
                case ToyRequestType.EnterToy:
                    return new ToyAesCryptoNoDecrypt(ToyByteUtil.HexToBytes(CommonAesKey));

                case ToyRequestType.GetUserInfo:
                    var npsn = session.Npsn;
                    var paddedNpsn = npsn.ToString("D19", CultureInfo.InvariantCulture);
                    var tmpKey = paddedNpsn[4..];
                    var tmpKeyBytes = Encoding.UTF8.GetBytes(tmpKey);
                    var encryptionKey = paddedNpsn[3..];
                    var key = ToyCrypto.Encrypt(encryptionKey, tmpKeyBytes);
                    return new ToyAesCrypto(key);

                case ToyRequestType.CheckEmailAccountRegistered:
                case ToyRequestType.EmailAccountSignUp:
                case ToyRequestType.EmailAccountResetPassword:
                case ToyRequestType.LoginWithEmail:
                case ToyRequestType.LoginWithGuest:
                    return new ToyAesCrypto(ToyByteUtil.HexToBytes(CommonAesKey));

                case ToyRequestType.GetEmailUserInfo:
                    return new ToyNoEncryption();

                default:
                    throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
        }
    }
}