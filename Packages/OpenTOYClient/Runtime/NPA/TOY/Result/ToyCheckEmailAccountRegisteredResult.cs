namespace NPA.TOY.Result
{
    internal class ToyCheckEmailAccountRegisteredResult : ToyResult
    {
        public ResultSet Result { get; set; }

        public class ResultSet
        {
            public int IsRegistered { get; set; }
        }
    }
}