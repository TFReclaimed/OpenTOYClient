using NPA.TOY.Request;
using NPA.TOY.Result;
using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToyLoginPasswordScreen : AbstractToyScreen
    {
        [SerializeField]
        private Text emailText;
        [SerializeField]
        private Text errorText;
        [SerializeField]
        private InputField passwordInput;
        [SerializeField]
        private Button forgotPasswordButton;
        [SerializeField]
        private Button loginButton;

        private void OnEnable()
        {
            emailText.text = NPAccount.Instance.session.Email;
            errorText.gameObject.SetActive(false);
            passwordInput.text = string.Empty;
        }

        private void Update()
        {
            loginButton.interactable = !string.IsNullOrEmpty(passwordInput.text);
        }

        public void OnForgotPasswordClicked()
        {
            toyUi.SetScreen(ToyScreen.ResetPassword);
        }

        public void OnContinueClicked()
        {
            toyUi.ShowLoading(true);
            errorText.gameObject.SetActive(false);

            var request = (ToyLoginRequest) ToyRequestFactory.CreateRequest(ToyRequestType.LoginWithEmail, NPAccount.Instance.session);
            request.UserId = NPAccount.Instance.session.Email;
            request.Passwd = passwordInput.text;
            request.MemType = (int) NPLoginType.NPLoginTypeEmail;
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

            //TODO
        }
    }
}