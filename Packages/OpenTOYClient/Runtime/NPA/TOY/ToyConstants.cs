using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NPA.TOY
{
    internal static class ToyConstants
    {
        public static readonly JsonSerializerSettings JsonSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }
}