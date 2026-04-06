using NPA.TOY.Request;
using NPA.TOY.Result;
using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToySignupScreen : AbstractToyScreen
    {
        [SerializeField]
        private Text emailText;
        [SerializeField]
        private Text errorText;
        [SerializeField]
        private InputField passwordInput;
        [SerializeField]
        private Button signupButton;

        private void OnEnable()
        {
            emailText.text = NPAccount.Instance.session.Email;
            errorText.gameObject.SetActive(false);
            passwordInput.text = "";
        }

        private void Update()
        {
            signupButton.interactable = !string.IsNullOrEmpty(passwordInput.text);
        }

        public void OnContinueClicked()
        {
            toyUi.ShowLoading(true);
            errorText.gameObject.SetActive(false);

            var request = (ToyEmailAccountSignUpRequest) ToyRequestFactory.CreateRequest(ToyRequestType.EmailAccountSignUp, NPAccount.Instance.session);
            request.Email = NPAccount.Instance.session.Email;
            request.Passwd = passwordInput.text;
            request.SetListener(OnResponse);
            NPAccount.Instance.mGameObject.ExecuteRequest(request);
        }

        private void OnResponse(ToyLoginResult result)
        {
            toyUi.ShowLoading(false);

            if (result.errorCode != 0)
            {
                errorText.text = result.errorText;
                errorText.gameObject.SetActive(true);
                return;
            }

            NPAccount.Instance.CloseUi();
        }
    }
}