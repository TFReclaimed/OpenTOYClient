namespace NPA.TOY.Result
{
    internal class ToyLoginResult : ToyResult
    {
        public ResultSet Result { get; set; }

        public class ResultSet
        {
            public long Npsn { get; set; }
            public string NpToken { get; set; }
        }
    }
}