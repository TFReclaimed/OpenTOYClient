using NPA.TOY.Request;
using NPA.TOY.Result;
using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToyLoginScreen : AbstractToyScreen
    {
        [SerializeField]
        private Text errorText;
        [SerializeField]
        private InputField emailInput;
        [SerializeField]
        private Button continueButton;

        private void OnEnable()
        {
            errorText.gameObject.SetActive(false);
            emailInput.text = "";
        }

        private void Update()
        {
            continueButton.interactable = !string.IsNullOrEmpty(emailInput.text);
        }

        public void OnContinueClicked()
        {
            toyUi.ShowLoading(true);
            errorText.gameObject.SetActive(false);
            NPAccount.Instance.session.Email = emailInput.text;

            var request = (ToyCheckEmailAccountRegisteredRequest) ToyRequestFactory.CreateRequest(ToyRequestType.CheckEmailAccountRegistered, NPAccount.Instance.session);
            request.Email = emailInput.text;
            request.SetListener(OnResponse);
            NPAccount.Instance.mGameObject.ExecuteRequest(request);
        }

        private void OnResponse(ToyCheckEmailAccountRegisteredResult result)
        {
            if (result.Result.IsRegistered != 1)
            {
                toyUi.SetScreen(ToyScreen.Signup);
            }
            else
            {
                toyUi.SetScreen(ToyScreen.LoginPassword);
            }

            toyUi.ShowLoading(false);
        }
    }
}