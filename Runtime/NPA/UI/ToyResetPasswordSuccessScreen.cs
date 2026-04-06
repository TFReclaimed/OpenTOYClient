using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToyResetPasswordSuccessScreen : AbstractToyScreen
    {
        [SerializeField]
        private Text text;

        private void OnEnable()
        {
            text.text = string.Format(text.text, NPAccount.Instance.session.Email);
        }

        public void OnBackButtonClicked()
        {
            toyUi.SetScreen(ToyScreen.LoginPassword);
        }
    }
}