using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NPA.TOY.Result;
using NPA.TOY.Tools.Crypto;
using NPA.TOY.Tools.PlatformInfo;
using UnityEngine.Networking;

namespace NPA.TOY.Request
{
    internal abstract class ToyRequest<TResult> : AbstractToyRequest where TResult : ToyResult
    {
        private readonly ToyRequestType _requestType;

        private readonly ToySession _session;

        private readonly IToyCrypto _crypto;

        private readonly Dictionary<string, string> _headers = new();

        private Action<TResult> _listener;

        public ToyRequest(ToyRequestType requestType, ToySession session, IToyCrypto crypto)
        {
            _requestType = requestType;
            _session = session;
            _crypto = crypto;
        }

        protected void PutHeader(string key, string value)
        {
            _headers[key] = value;
        }

        public void SetListener(Action<TResult> listener)
        {
            _listener = listener;
        }

        protected virtual void OnPostExecute(TResult result)
        {
            _listener?.Invoke(result);
        }

        public override IEnumerator Execute()
        {
            var jsonData = JsonConvert.SerializeObject(this);
            var encryptedData = _crypto.Encrypt(Encoding.UTF8.GetBytes(jsonData));

            var path = GetRequestPath(_requestType);
            var uwr = new UnityWebRequest(ToyRequestFactory.ToyInfo.ToyUrl + path, UnityWebRequest.kHttpVerbPOST);
            var uploadHandler = new UploadHandlerRaw(encryptedData);
            uploadHandler.contentType = "application/x-www-form-urlencoded";
            uwr.uploadHandler = uploadHandler;
            uwr.downloadHandler = new DownloadHandlerBuffer();

            var npParams = new Dictionary<string, string>
            {
                { "sdkVer", ToyVersion.VERSION },
                { "os", ToyPlatformInfo.Instance.GetOs() },
                { "svcID", _session.ServiceId },
                { "npToken", _session.NpToken }
            };
            var npParamsJson = JsonConvert.SerializeObject(npParams);
            var encryptedNpParams = ToyByteUtil.BytesToHex(_crypto.Encrypt(Encoding.UTF8.GetBytes(npParamsJson)));

            uwr.SetRequestHeader("npparams", encryptedNpParams);
            uwr.SetRequestHeader("acceptLanguage", "en_US");
            uwr.SetRequestHeader("acceptCountry", "US");
            uwr.SetRequestHeader("uuid", ToyPlatformInfo.Instance.GetUuid());
            uwr.SetRequestHeader("npsn", _session.Npsn.ToString());

            foreach (var header in _headers)
            {
                uwr.SetRequestHeader(header.Key, header.Value);
            }

            yield return uwr.SendWebRequest();

            var responseData = uwr.downloadHandler.data;
            var decryptedData = _crypto.Decrypt(responseData);
            var jsonResponse = Encoding.UTF8.GetString(decryptedData);
            var response = JsonConvert.DeserializeObject<TResult>(jsonResponse);

            OnPostExecute(response);
        }

        private static string GetRequestPath(ToyRequestType requestType)
        {
            return requestType switch
            {
                ToyRequestType.GetUserInfo => "/sdk/getUserInfo.nx",
                ToyRequestType.CheckEmailAccountRegistered => "/sdk/isRegisteredNPAA.nx",
                ToyRequestType.EmailAccountSignUp => "/sdk/signUpNPAA.nx",
                ToyRequestType.EmailAccountResetPassword => "/sdk/requestResetPasswordNPAA.nx",
                ToyRequestType.GetEmailUserInfo => "/auth/me.nx",
                ToyRequestType.EnterToy => "/sdk/enterToy.nx",
                ToyRequestType.LoginWithEmail => "/sdk/signIn.nx",
                ToyRequestType.LoginWithGuest => "/sdk/signIn.nx",
                _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
            };
        }
    }
}