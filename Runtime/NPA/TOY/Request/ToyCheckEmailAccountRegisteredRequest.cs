using Newtonsoft.Json;
using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyCheckEmailAccountRegisteredRequest : ToyRequest<ToyCheckEmailAccountRegisteredResult>
    {
        [JsonProperty("userID")]
        public string Email { get; set; }

        public ToyCheckEmailAccountRegisteredRequest(ToySession session, IToyCrypto crypto)
            : base(ToyRequestType.CheckEmailAccountRegistered, session, crypto)
        {
        }
    }
}