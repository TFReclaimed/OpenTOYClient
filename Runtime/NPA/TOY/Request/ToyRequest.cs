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
    internal class ToyRequest<TResult> where TResult : ToyResult
    {
        private readonly ToyRequestType _requestType;

        private readonly IToyCrypto _crypto;

        private readonly Dictionary<string, string> _headers = new();

        private Action<TResult> _listener;

        public ToyRequest(ToyRequestType requestType, IToyCrypto crypto)
        {
            _requestType = requestType;
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

        public IEnumerator Execute()
        {
            var jsonData = JsonConvert.SerializeObject(this);
            var encryptedData = _crypto.Encrypt(Encoding.UTF8.GetBytes(jsonData));

            var uwr = new UnityWebRequest("https://opentoy.tfflinternal.com/sdk/enterToy.nx", UnityWebRequest.kHttpVerbPOST);//TODO
            var uploadHandler = new UploadHandlerRaw(encryptedData);
            uploadHandler.contentType = "application/x-www-form-urlencoded";
            uwr.uploadHandler = uploadHandler;
            uwr.downloadHandler = new DownloadHandlerBuffer();

            var npParams = new Dictionary<string, string>
            {
                { "sdkVer", ToyVersion.VERSION },
                { "os", ToyPlatformInfo.Instance.GetOs() },
                { "svcID", "1253" },//TODO
                { "npToken", "" }//TODO
            };
            var npParamsJson = JsonConvert.SerializeObject(npParams);
            var encryptedNpParams = ToyByteUtil.BytesToHex(_crypto.Encrypt(Encoding.UTF8.GetBytes(npParamsJson)));

            uwr.SetRequestHeader("npparams", encryptedNpParams);
            uwr.SetRequestHeader("acceptLanguage", "en_US");
            uwr.SetRequestHeader("acceptCountry", "US");
            uwr.SetRequestHeader("uuid", ToyPlatformInfo.Instance.GetUuid());
            uwr.SetRequestHeader("npsn", "0");//TODO

            foreach (var header in _headers)
            {
                uwr.SetRequestHeader(header.Key, header.Value);
            }

            yield return uwr.SendWebRequest();

            var responseData = uwr.downloadHandler.data;
            var decryptedData = _crypto.Decrypt(responseData);
            var jsonResponse = Encoding.UTF8.GetString(decryptedData);
            var response = JsonConvert.DeserializeObject<TResult>(jsonResponse);

            _listener?.Invoke(response);
        }
    }
}