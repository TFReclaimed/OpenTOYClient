using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;

namespace NPA.TOY.Request
{
    internal class ToyEnterRequest : ToyRequest<ToyEnterResult>
    {
        public int Mcc { get; set; }
        public int Mnc { get; set; }

        public ToyEnterRequest(ToySession session, IToyCrypto crypto) : base(ToyRequestType.EnterToy, session, crypto)
        {
        }
    }
}