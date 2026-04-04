using System;
using UnityEngine;

namespace NPA.TOY
{
    internal static class LocaleManager
    {
        public static NPCountry GetCountry()
        {
            var countryStr = PlayerPrefs.GetString("ToyCountry", "");
            if (string.IsNullOrWhiteSpace(countryStr))
            {
                var systemLocale = LocaleFromSystemLanguage();
                return CountryFromLocale(systemLocale);
            }

            if (Enum.TryParse(countryStr, out NPCountry npCountry))
            {
                return npCountry;
            }

            return NPCountry.UnitedStates;
        }

        public static void SetCountry(NPCountry country)
        {
            PlayerPrefs.SetString("ToyCountry", country.ToString());
            PlayerPrefs.Save();
        }

        public static NPCountry CountryFromLocale(NPLocale locale)
        {
            // TODO: Map NPLocale to NPCountry
            return NPCountry.UnitedStates;
        }

        public static NPLocale LocaleFromSystemLanguage()
        {
            // TODO: Map Application.SystemLanguage to NPLocale
            return NPLocale.EN_US;
        }
    }
}