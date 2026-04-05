using Newtonsoft.Json;
using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyEmailAccountSignUpRequest : ToyRequest<ToyLoginResult>
    {
        public string Uuid2 { get; set; }
        [JsonProperty("userID")]
        public string Email { get; set; }
        public string Passwd { get; set; }

        public ToyEmailAccountSignUpRequest(ToySession session, IToyCrypto crypto)
            : base(ToyRequestType.EmailAccountSignUp, session, crypto)
        {
        }

        protected override void OnPostExecute(ToyLoginResult result)
        {
            if (result.errorCode == 0)
            {
                session.Npsn = result.Result.Npsn;
                session.NpToken = result.Result.NpToken;
            }

            base.OnPostExecute(result);
        }
    }
}