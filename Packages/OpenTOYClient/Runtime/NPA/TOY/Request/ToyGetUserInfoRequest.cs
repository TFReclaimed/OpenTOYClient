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

        protected override void OnPostExecute(ToyGetUserInfoResult result)
        {
            result.Result.Npsn = session.Npsn;
            result.Result.NpToken = session.NpToken;
            base.OnPostExecute(result);
        }
    }
}