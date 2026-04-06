using System.Collections.Generic;
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

        public string Email { get; set; }

        public string ServiceId { get; set; }

        public List<int> AvailableMemberships { get; set; } = new();

        private long _npsn;
        private string _npToken;

        public ToySession()
        {
            Npsn = PlayerPrefs.GetInt("ToyNpsn", 0);
            NpToken = PlayerPrefs.GetString("ToyNpToken", string.Empty);
        }
    }
}