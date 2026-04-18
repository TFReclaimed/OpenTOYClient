using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPA.UI.Toast
{
    internal class ToastManager : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup toastCanvasGroup;
        [SerializeField]
        private Text toastText;

        private static ToastManager _instance;

        private readonly Queue<ToastData> _toastQueue = new();

        private bool _isShowing;

        private static void CreateInstance()
        {
            var toastUi = Resources.Load<GameObject>("ToastUI");
            if (toastUi == null)
            {
                Debug.LogError("ToastUI prefab not found in Resources");
                return;
            }

            var toastObj = Instantiate(toastUi);
            DontDestroyOnLoad(toastObj);
            _instance = toastObj.GetComponent<ToastManager>();
        }

        public static void ShowToast(string message, float duration = 2f)
        {
            if (_instance == null)
            {
                CreateInstance();
            }

            _instance._toastQueue.Enqueue(new ToastData
            {
                Message = message,
                Duration = duration
            });

            if (!_instance._isShowing)
            {
                _instance.StartCoroutine(_instance.ProcessQueue());
            }
        }

        private IEnumerator ProcessQueue()
        {
            _isShowing = true;

            while (_toastQueue.Count > 0)
            {
                var toast = _toastQueue.Dequeue();
                toastText.text = toast.Message;

                yield return Fade(0f, 1f, 0.5f);
                yield return new WaitForSeconds(toast.Duration);
                yield return Fade(1f, 0f, 0.5f);
                yield return new WaitForSeconds(0.1f);
            }

            _isShowing = false;
        }

        private IEnumerator Fade(float start, float end, float time)
        {
            var elapsed = 0f;
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                toastCanvasGroup.alpha = Mathf.Lerp(start, end, elapsed / time);
                yield return null;
            }

            toastCanvasGroup.alpha = end;
        }
    }
}