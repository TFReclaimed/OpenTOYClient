using NPA.TOY.Result.Model;

namespace NPA.TOY.Result
{
    internal class ToyGetUserInfoResult : ToyResult
    {
        public ResultSet Result { get; set; }

        public class ResultSet
        {
            public bool DoToast { get; set; }
            public ToyUserInfo NpsnUserInfo { get; set; }
        }
    }
}