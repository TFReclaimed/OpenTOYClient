using NPA.TOY.Result.Model;

namespace NPA.TOY.Result
{
    internal class ToyEnterResult : ToyResult
    {
        public ResultSet Result { get; set; }

        public class ResultSet
        {
            public string Country { get; set; }
            public ToyService Service { get; set; }
        }
    }
}