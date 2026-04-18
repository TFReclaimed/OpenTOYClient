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
                PlayerPrefs.SetString("ToyNpsn", value.ToString());
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
            Npsn = long.TryParse(PlayerPrefs.GetString("ToyNpsn", "0"), out var npsn) ? npsn : 0;
            NpToken = PlayerPrefs.GetString("ToyNpToken", string.Empty);
        }

        public void Logout()
        {
            Npsn = 0;
            NpToken = string.Empty;
            Email = string.Empty;
            PlayerPrefs.Save();
        }
    }
}