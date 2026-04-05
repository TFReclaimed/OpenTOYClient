using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyGetUserInfoRequest : ToyRequest<ToyGetUserInfoResult>
    {
        public ToyGetUserInfoRequest(ToySession session, IToyCrypto crypto)
            : base(ToyRequestType.GetUserInfo, session, crypto)
        {
        }
    }
}