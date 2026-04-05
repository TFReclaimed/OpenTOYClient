using Newtonsoft.Json;
using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyLoginRequest : ToyRequest<ToyLoginResult>
    {
        public string Uuid2 { get; set; }
        public string UserId { get; set; }
        public string Passwd { get; set; }
        [JsonProperty("optional")]
        public DeviceInfo Device { get; set; }

        public ToyLoginRequest(ToyRequestType requestType, ToySession session, IToyCrypto crypto)
            : base(requestType, session, crypto)
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

        public class DeviceInfo
        {
            public string Device { get; set; }
        }
    }
}