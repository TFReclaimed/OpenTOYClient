using UnityEngine;

namespace NPA.TOY
{
    internal class ToySession
    {
        public long Npsn
        {
            get => _npsn;
            set
            {
                PlayerPrefs.SetInt("ToyNpsn", (int) value);
                _npsn = value;
            }
        }

        public string NpToken
        {
            get => _npToken;
            set
            {
                PlayerPrefs.SetString("ToyNpToken", value);
                _npToken = value;
            }
        }

        public string ServiceId { get; set; }

        private long _npsn;
        private string _npToken;

        public ToySession()
        {
            Npsn = PlayerPrefs.GetInt("ToyNpsn", 0);
            NpToken = PlayerPrefs.GetString("ToyNpToken", string.Empty);
        }
    }
}