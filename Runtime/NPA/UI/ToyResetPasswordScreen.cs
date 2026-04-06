using NPA.TOY.Request;
using NPA.TOY.Result;
using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToyResetPasswordScreen : AbstractToyScreen
    {
        [SerializeField]
        private Text errorText;
        [SerializeField]
        private InputField emailInput;
        [SerializeField]
        private Button resetButton;

        private void OnEnable()
        {
            errorText.gameObject.SetActive(false);
            emailInput.text = NPAccount.Instance.session.Email;
        }

        private void Update()
        {
            resetButton.interactable = !string.IsNullOrEmpty(emailInput.text);
        }

        public void OnContinueClicked()
        {
            toyUi.ShowLoading(true);
            errorText.gameObject.SetActive(false);

            var request = (ToyEmailAccountResetPasswordRequest) ToyRequestFactory.CreateRequest(ToyRequestType.EmailAccountResetPassword, NPAccount.Instance.session);
            request.Email = emailInput.text;
            request.SetListener(OnResponse);
            NPAccount.Instance.mGameObject.ExecuteRequest(request);
        }

        private void OnResponse(ToyResult result)
        {
            toyUi.ShowLoading(false);

            if (result.errorCode != 0)
            {
                errorText.text = result.errorDetail;
                errorText.gameObject.SetActive(true);
                return;
            }

            toyUi.SetScreen(ToyScreen.ResetPasswordSuccess);
        }
    }
}