using Newtonsoft.Json;
using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyEmailAccountResetPasswordRequest : ToyRequest<ToyResult>
    {
        [JsonProperty("userID")]
        public string Email { get; set; }

        public ToyEmailAccountResetPasswordRequest(ToySession session, IToyCrypto crypto)
            : base(ToyRequestType.EmailAccountResetPassword, session, crypto)
        {
        }
    }
}