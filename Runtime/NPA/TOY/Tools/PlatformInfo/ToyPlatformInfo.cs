using System;
using UnityEngine;

namespace NPA.TOY.Tools.PlatformInfo
{
    internal class ToyPlatformInfo : IToyPlatformInfo
    {
        private static IToyPlatformInfo _instance;

        public static IToyPlatformInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToyPlatformInfo();
                }

                return _instance;
            }
        }

        public NPCountry GetCountry()
        {
            return LocaleManager.GetCountry();
        }

        public NPLocale GetLocale()
        {
            return LocaleManager.LocaleFromSystemLanguage();
        }

        public string GetModel()
        {
            return SystemInfo.deviceModel;
        }

        public string GetOs()
        {
#if UNITY_ANDROID
            return "A";
#elif UNITY_IOS
            return "I";
#else
            return "U";
#endif
        }

        public string GetUuid()
        {
            var uuid = PlayerPrefs.GetString("ToyUuid", "");
            if (!string.IsNullOrWhiteSpace(uuid))
            {
                return uuid;
            }

            uuid = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("ToyUuid", uuid);
            PlayerPrefs.Save();
            return uuid;
        }

        public string GetUuid2()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
}