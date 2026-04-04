namespace NPA.TOY.Tools.PlatformInfo
{
    internal interface IToyPlatformInfo
    {
        NPCountry GetCountry();
        NPLocale GetLocale();
        string GetModel();
        string GetOs();
        string GetUuid();
        string GetUuid2();
    }
}