using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI
{
    public class ToyUi : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private GameObject content;
        [SerializeField]
        private GameObject loadingOverlay;

        private ToyScreen _currentScreen;

        public void SetScreen(ToyScreen screen)
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }

            if (screen == ToyScreen.None)
            {
                return;
            }

            titleText.text = screen switch
            {
                ToyScreen.Login or ToyScreen.LoginPassword => "Log in",
                ToyScreen.Signup => "Sign up",
                ToyScreen.ResetPassword or ToyScreen.ResetPasswordSuccess => "Reset Password",
                _ => titleText.text
            };

            var prefabName = screen switch
            {
                ToyScreen.Login => "Login Screen",
                ToyScreen.LoginPassword => "Login Password Screen",
                ToyScreen.Signup => "Signup Screen",
                ToyScreen.ResetPassword => "Reset Password Screen",
                ToyScreen.ResetPasswordSuccess => "Reset Password Success Screen",
                _ => null
            };

            if (prefabName == null)
            {
                Debug.LogError($"No prefab defined for screen {screen}");
                OnCloseClicked();
                return;
            }

            var prefab = Resources.Load<GameObject>(prefabName);
            if (prefab == null)
            {
                Debug.LogError($"Prefab {prefabName} not found in Resources");
                OnCloseClicked();
                return;
            }

            _currentScreen = screen;

            var screenInstance = Instantiate(prefab, content.transform).GetComponent<AbstractToyScreen>();
            screenInstance.Initialize(this);
        }

        public void ShowLoading(bool show)
        {
            loadingOverlay.SetActive(show);
        }

        public void OnBackClicked()
        {
            switch (_currentScreen)
            {
                case ToyScreen.LoginPassword:
                case ToyScreen.Signup:
                    SetScreen(ToyScreen.Login);
                    break;

                case ToyScreen.ResetPassword:
                case ToyScreen.ResetPasswordSuccess:
                    SetScreen(ToyScreen.LoginPassword);
                    break;

                default:
                    OnCloseClicked();
                    break;
            }
        }

        public void OnCloseClicked()
        {
            _currentScreen = ToyScreen.None;
            NPAccount.Instance.CloseUi();
        }
    }
}