using NPA.TOY.Result.Model;

namespace NPA.TOY.Result
{
    internal class ToyEmailUserInfoResult : ToyResult
    {
        public string Email { get; set; }
        public ToyEmailUserInfo Extend { get; set; }
    }
}