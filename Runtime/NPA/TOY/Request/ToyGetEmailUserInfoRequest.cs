using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyGetEmailUserInfoRequest : ToyRequest<ToyEmailUserInfoResult>
    {
        public ToyGetEmailUserInfoRequest(ToySession session, IToyCrypto crypto)
            : base(ToyRequestType.GetEmailUserInfo, session, crypto)
        {
        }
    }
}