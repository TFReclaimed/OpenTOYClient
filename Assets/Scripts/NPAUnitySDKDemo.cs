using System.Collections.Generic;
using NPA;
using SimpleJSON;
using UnityEngine;

public class NPAUnitySDKDemo : MonoBehaviour, INPListener, INPRecvNotificationListener, INPGCMListener,
    INPBannerListener, INPEndingBannerListener, INPOnCloseListener, INPPlateListener, INPRuntimePermissionListener
{
    public string SENDER_ID = "660640960303";

    private string _msg;

    private Vector2 _scrollPositionForAPIButton = Vector2.zero;

    private float _apiButtonWidth;

    private float _apiButtonHeight;

    private int _viewMode;

    private NPAccount _account;

    private int _hasNext;

    private bool _bFbConnect;

    private bool _bGpConnect;

    private bool _bTwConnect;

    private void Start()
    {
        _apiButtonWidth = Screen.width * 0.5f;
        _apiButtonHeight = Screen.height * 0.08f;
        _account = NPAccount.Instance;
        Debug.Log("Register Push OnStart");
        _account.pushInit(this, SENDER_ID);
        _account.EnterToy(this);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _account.ShowEndingBanner(this);
        }
    }

    private void OnGUI()
    {
        var position = new Rect(Screen.width * 0f, Screen.height * 0f, Screen.width * 0.6f,
            Screen.height * 0.4f);
        GUI.TextArea(position, _msg);
        position = new Rect(Screen.width * 0.6f, Screen.height * 0f, Screen.width * 0.4f,
            Screen.height * 0.1f);
        if (GUI.Button(position, "Auth"))
        {
            _viewMode = 0;
        }

        position = new Rect(Screen.width * 0.6f, Screen.height * 0.1f, Screen.width * 0.4f,
            Screen.height * 0.1f);
        if (GUI.Button(position, "Other"))
        {
            _viewMode = 1;
        }

        position = new Rect(Screen.width * 0.6f, Screen.height * 0.2f, Screen.width * 0.4f,
            Screen.height * 0.1f);
        if (GUI.Button(position, "SNSConnect"))
        {
            _viewMode = 2;
            _account.getSnsConnectionStatus(this);
        }

        position = new Rect(Screen.width * 0.6f, Screen.height * 0.3f, Screen.width * 0.4f,
            Screen.height * 0.1f);
        if (GUI.Button(position, "GoogleGameSVC"))
        {
            _viewMode = 3;
        }

        position = new Rect(Screen.width * 0f, Screen.height * 0.4f, Screen.width * 0.96f,
            Screen.height * 0.6f);
        _scrollPositionForAPIButton = GUI.BeginScrollView(position, _scrollPositionForAPIButton,
            new Rect(0f, 0f, Screen.width * 1f, Screen.height * 2f));
        switch (_viewMode)
        {
            case 0:
                GUIForAuth();
                break;
            case 1:
                GUIForOther();
                break;
            case 2:
                GUIForSnsConnect();
                break;
            case 3:
                GUIForGoogleGameService();
                break;
        }

        GUI.EndScrollView();
    }

    private Rect RectForButton(int cellX, int cellY)
    {
        return new Rect(_apiButtonWidth * cellX, _apiButtonHeight * cellY, _apiButtonWidth, _apiButtonHeight);
    }

    private void GUIForAuth()
    {
        if (GUI.Button(RectForButton(0, 0), "Login"))
        {
            _account.Login(this);
        }

        if (GUI.Button(RectForButton(1, 0), "UserInfo"))
        {
            _account.GetUserInfo(this);
        }

        if (GUI.Button(RectForButton(1, 1), "Logout"))
        {
            _account.Logout(this);
        }

        if (GUI.Button(RectForButton(0, 2), "UnregisterService"))
        {
            _account.UnregisterService(this);
        }

        if (GUI.Button(RectForButton(1, 2), "recoverUser"))
        {
            _account.RecoverUser(this);
        }

        if (GUI.Button(RectForButton(0, 3), "GuestLogin"))
        {
            _account.Login(NPLoginType.NPLoginTypeGuest, this);
        }

        if (GUI.Button(RectForButton(1, 3), "FriendList(Friends)"))
        {
            _account.GetFriends(_hasNext, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(0, 4), "FriendList(Invites)"))
        {
            _account.GetFriends(_hasNext, NPAccount.FRIEND_FILTER_TYPE_INVITES, this);
        }

        if (GUI.Button(RectForButton(1, 4), "GetLoginType"))
        {
            _msg = "logintype = " + _account.GetLoginType();
        }

        if (GUI.Button(RectForButton(0, 5), "LoginForKakao"))
        {
            _account.LoginForKakao("38933893", "KAKAO_ACCESSTOKEN_OR_EMPTYSTRING", this);
        }

        if (GUI.Button(RectForButton(1, 5), "ShowTermsOfAgree"))
        {
            _account.ShowTermsOfAgree(this);
        }

        if (GUI.Button(RectForButton(0, 6), "RegisterPush"))
        {
            _account.RegisterPush(this);
        }

        if (GUI.Button(RectForButton(0, 7), "showDataBackup"))
        {
            _account.ShowDataBackup(this);
        }

        if (GUI.Button(RectForButton(1, 7), "showDataRestore"))
        {
            _account.ShowDataRestore(this);
        }

        if (GUI.Button(RectForButton(0, 8), "enterToy"))
        {
            _account.EnterToy(this);
        }

        if (GUI.Button(RectForButton(1, 8), "showSettlementFund"))
        {
            _account.ShowSettlementFund("Item9494", "4000$", this);
        }

        if (GUI.Button(RectForButton(0, 9), "NXLogin"))
        {
            _account.NXLogin("ID", "PW", this);
        }

        if (GUI.Button(RectForButton(1, 9), "GetNexonSN"))
        {
            _account.GetNexonSN(this);
        }
    }

    private void GUIForOther()
    {
        if (GUI.Button(RectForButton(0, 0), "Banner"))
        {
            _account.ShowBanner("1", this);
        }

        if (GUI.Button(RectForButton(1, 0), "ShowNotice"))
        {
            _account.ShowNotice(this);
        }

        if (GUI.Button(RectForButton(0, 1), "ShowFAQ"))
        {
            _account.ShowFAQ();
        }

        if (GUI.Button(RectForButton(1, 1), "ConditionalBanner"))
        {
            _account.GetPromotion("placement_1", this);
        }

        if (GUI.Button(RectForButton(0, 2), "Global CS"))
        {
            var info = new NPCSInfo();
            info["GameID"] = "5";
            info["ChracterName"] = "A";
            info.questionInfos = new[] { "GameVersion", "CHRACTERID" };
            _account.ShowCustomerService(info);
        }

        if (GUI.Button(RectForButton(1, 2), "NP Plate"))
        {
            var info = new NPCSInfo();
            info["GameID"] = "5";
            info["ChracterName"] = null;
            info.questionInfos = new[] { "GameVersion", "CHRACTERID" };
            _account.ShowPlate(0, info, this);
        }

        if (GUI.Button(RectForButton(0, 3), "Share"))
        {
            _account.Share("NexonPlay", "Come Together", "http://m.nexon.com");
        }

        if (GUI.Button(RectForButton(1, 3), "LocalPush Reserve"))
        {
            var time = new NPNotificationTime(0, 0, 0, 0, 1, 0);
            var data = new NPNotificationData(121, "121 - NPA Local Push Test for Android A-yo!!", "{\"t1\":\"t2\"}",
                time, NPAccount.LOCAL_PUSH_TYPE_AFTER);
            _account.dispatchLocalPush(data);
            var time2 = new NPNotificationTime(2014, 9, 23, 10, 1, 12);
            var data2 = new NPNotificationData(122, "122 - NPA Local Push Test for Android A-yo!!", string.Empty, time2,
                NPAccount.LOCAL_PUSH_TYPE_ON);
            _account.dispatchLocalPush(data2);
            _msg = "LocalPush Reserve Success";
        }

        if (GUI.Button(RectForButton(0, 4), "LocalPush Cancel"))
        {
            _account.cancelLocalPush(122);
            _msg = "LocalPush Cancel Success";
        }

        if (GUI.Button(RectForButton(1, 4), "LocalPush AllCancel"))
        {
            _account.cancelAllLocalPush();
            _msg = "LocalPush AllCancel Success";
        }

        if (GUI.Button(RectForButton(0, 5), "getCountry"))
        {
            _msg = "Country: " + _account.getCountry();
        }

        if (GUI.Button(RectForButton(1, 5), "getLocale"))
        {
            _msg = "Locale: " + _account.getLocale();
        }

        if (GUI.Button(RectForButton(0, 6), "setCountry"))
        {
            var country = _account.getCountry();
            if (country == NPCountry.Korea)
            {
                _account.setCountry(NPCountry.UnitedStates);
            }
            else
            {
                _account.setCountry(NPCountry.Korea);
            }

            _msg = "Country Setting Success - Result: " + _account.getCountry();
        }

        if (GUI.Button(RectForButton(1, 6), "setLocale"))
        {
            var locale = _account.getLocale();
            if (locale == NPLocale.KO_KR)
            {
                _account.setLocale(NPLocale.EN_US);
            }
            else
            {
                _account.setLocale(NPLocale.KO_KR);
            }

            _msg = "Locale Setting Success - Result: " + _account.getLocale();
        }

        if (GUI.Button(RectForButton(0, 7), "sendEvent(AppsFlyer)"))
        {
            _account.SendEvent(string.Empty, "UNITY", "EVENT", string.Empty);
            _account.SendEcommerceItem(string.Empty, string.Empty, string.Empty, string.Empty, 92.0, 0L, "USD");
        }

        if (GUI.Button(RectForButton(1, 7), "getCountryFromServer"))
        {
            _account.GetCountryFromServer(this);
        }

        if (GUI.Button(RectForButton(0, 8), "getUUID"))
        {
            _msg = string.Empty + _account.getUUID();
        }

        if (GUI.Button(RectForButton(1, 8), "FBLogTest"))
        {
            Debug.Log("FBACTIVATE!!!!");
            _account.FBActivateApp();
            _account.FBDeactivateApp();
            Debug.Log("FBPURCHASE!!!!");
            _account.FBLogPurchase(2.22, "USD", null);
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("mygame_name", "mygame");
            _account.FBLogPurchase(2.22, "USD", dictionary);
            Debug.Log("FBEVENT!!!!");
            _account.FBLogEvent("fb_mobile_tutorial_completion");
            _account.FBLogEvent("EVENT2", 202.2);
            _account.FBLogEvent("EVENT3", dictionary);
            _account.FBLogEvent("EVENT4", 202.3, dictionary);
        }

        if (GUI.Button(RectForButton(0, 9), "GetServiceInfo"))
        {
            _account.GetServiceInfo(this);
        }

        if (GUI.Button(RectForButton(1, 9), "Push & SMS"))
        {
            _account.ShowPushNSms(this);
        }

        if (GUI.Button(RectForButton(0, 10), "Runtime Permission"))
        {
            var list = new List<string>();
            list.Add("android.permission.READ_PHONE_STATE");
            list.Add("android.permission.READ_EXTERNAL_STORAGE");
            _account.RequestPermissions(list, 10, string.Empty, this);
        }

        if (GUI.Button(RectForButton(1, 10), "GetAdvertisingId"))
        {
            _account.GetAdvertisingId(this);
        }

        if (GUI.Button(RectForButton(0, 11), "EventWebView"))
        {
            _account.ShowEventWeb("http://m.nexon.co.kr");
        }
    }

    private void GUIForSnsConnect()
    {
        if (GUI.Button(RectForButton(0, 0), "FB:" + _bFbConnect))
        {
            if (_bFbConnect)
            {
                _account.snsDisconnect(NPSnsType.NPSnsTypeFaceBook, this);
            }
            else
            {
                _account.snsConnect(NPSnsType.NPSnsTypeFaceBook, this);
            }
        }

        if (GUI.Button(RectForButton(1, 0), "FBUserinfo"))
        {
            _account.getSnsUserInfo(NPSnsType.NPSnsTypeFaceBook, this);
        }

        if (GUI.Button(RectForButton(0, 1), "FBFriend"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeFaceBook, 0, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(1, 1), "FBFriendNext"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeFaceBook, 1, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(0, 2), "GP:" + _bGpConnect))
        {
            if (_bGpConnect)
            {
                _account.snsDisconnect(NPSnsType.NPSnsTypeGooglePlus, this);
            }
            else
            {
                _account.snsConnect(NPSnsType.NPSnsTypeGooglePlus, this);
            }
        }

        if (GUI.Button(RectForButton(1, 2), "GPUserinfo"))
        {
            _account.getSnsUserInfo(NPSnsType.NPSnsTypeGooglePlus, this);
        }

        if (GUI.Button(RectForButton(0, 3), "GPFriend"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeGooglePlus, 0, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(1, 3), "GPFriendNext"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeGooglePlus, 1, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(0, 4), "TW:" + _bTwConnect))
        {
            if (_bTwConnect)
            {
                _account.snsDisconnect(NPSnsType.NPSnsTypeTwitter, this);
            }
            else
            {
                _account.snsConnect(NPSnsType.NPSnsTypeTwitter, this);
            }
        }

        if (GUI.Button(RectForButton(1, 4), "TWUserinfo"))
        {
            _account.getSnsUserInfo(NPSnsType.NPSnsTypeTwitter, this);
        }

        if (GUI.Button(RectForButton(0, 5), "TWFriend"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeTwitter, 0, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(1, 5), "TWFriendNext"))
        {
            _account.GetFriends(NPSnsType.NPSnsTypeTwitter, 1, NPAccount.FRIEND_FILTER_TYPE_FRIENDS, this);
        }

        if (GUI.Button(RectForButton(0, 6), "FB FetchDeferredAppLink"))
        {
            _account.FBFetchDeferredAppLink(this);
        }

        if (GUI.Button(RectForButton(1, 6), "FB AppInvite"))
        {
            _account.FBAppInvite("https://fb.me/719770911498202", "http://www.kccosd.org/files/testing_image.jpg", this);
        }

        if (GUI.Button(RectForButton(0, 7), "FB Share"))
        {
            _account.FBShare("Play Account", "공유합니다", "https://fb.me/719770911498202",
                "http://www.kccosd.org/files/testing_image.jpg", this);
        }

        if (GUI.Button(RectForButton(0, 8), "FB Friends"))
        {
            _account.FBGetFriends(0, this);
        }

        if (GUI.Button(RectForButton(1, 8), "FB Friends Next"))
        {
            _account.FBGetFriends(1, this);
        }

        if (GUI.Button(RectForButton(0, 9), "FB Debug On"))
        {
            _account.FBSetIsDebugEnabled(true);
        }

        if (GUI.Button(RectForButton(1, 9), "FB Debug Off"))
        {
            _account.FBSetIsDebugEnabled(false);
        }
    }

    private void GUIForGoogleGameService()
    {
        if (GUI.Button(RectForButton(0, 0), "showAchievement"))
        {
            _account.showAchievement(this);
        }

        if (GUI.Button(RectForButton(1, 0), "showLeaderboard"))
        {
            _account.showLeaderBoard("CgkIr56Uip0TEAIQBg", this);
        }

        if (GUI.Button(RectForButton(0, 1), "unlock"))
        {
            _account.unlockAchievementImmediate("CgkIr56Uip0TEAIQAw", this);
        }

        if (GUI.Button(RectForButton(1, 1), "increment"))
        {
            _account.incrementAchievementImmediate("CgkIr56Uip0TEAIQAQ", 100, this);
        }

        if (GUI.Button(RectForButton(0, 2), "submitScore"))
        {
            _account.submitScoreImmediate("CgkIr56Uip0TEAIQBg", 1212L, this);
        }

        if (GUI.Button(RectForButton(1, 2), "acheivementList"))
        {
            _account.loadAchievementData(true, this);
        }

        if (GUI.Button(RectForButton(0, 3), "setStepsAchievement"))
        {
            _account.setStepsAchievementImmediate("CgkIr56Uip0TEAIQBw", 1, this);
        }

        if (GUI.Button(RectForButton(1, 3), "showAllLeaderBoard"))
        {
            _account.showAllLeaderBoard(this);
        }

        if (GUI.Button(RectForButton(0, 4), "currentPlayerScore"))
        {
            _account.loadCurrentPlayerLeaderboardScore("CgkIr56Uip0TEAIQBg", 2, 1, this);
        }

        if (GUI.Button(RectForButton(0, 5), "connectGamePlatform"))
        {
            _account.ConnectGamePlatform(this);
        }

        if (GUI.Button(RectForButton(1, 5), "disconnectGamePlatform"))
        {
            _account.DisconnectGamePlatform(this);
        }

        if (GUI.Button(RectForButton(0, 6), "isEnableGamePlatform"))
        {
            _msg = string.Empty + _account.IsEnableGamePlatform();
        }

        if (GUI.Button(RectForButton(1, 6), "screenCapture"))
        {
            _account.ScreenCapture(this);
        }

        if (GUI.Button(RectForButton(0, 7), "getPlayerStats"))
        {
            _account.GetPlayerStats(false, this);
        }

        if (GUI.Button(RectForButton(1, 7), "LogoutGamePlatform"))
        {
            _account.LogoutGamePlatform(this);
        }
    }

    public void OnResult(NPResult npResult)
    {
        var asInt = npResult.resultJson["errorCode"].AsInt;
        var text = npResult.resultJson.ToString();
        switch (npResult.requestTag)
        {
            case NPRequestTypeTag.NPRequestTypeEnterToy:
                if (asInt == 0)
                {
                    _msg = "SUCCESS" + text;
                }
                else if (_account.isAuthCrashError(asInt))
                {
                    _msg = "FAILED" + text;
                }
                else if (asInt == -9)
                {
                    _msg = "FAILED" + text;
                }
                else
                {
                    _msg = "FAILED" + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeLoginWithNX:
            case NPRequestTypeTag.NPRequestTypeLogin:
            case NPRequestTypeTag.NPRequestTypeLoginWithNXNet:
            case NPRequestTypeTag.NPRequestTypeLoginWithNaver:
            case NPRequestTypeTag.NPRequestTypeLoginWithTwitter:
            case NPRequestTypeTag.NPRequestTypeLoginWithNaverChannel:
            case NPRequestTypeTag.NPRequestTypeLoginWithKakao:
            case NPRequestTypeTag.NPRequestTypeLoginWithGPlus:
            case NPRequestTypeTag.NPRequestTypeLoginWithFB:
            case NPRequestTypeTag.NPRequestTypeLoginWithGuest:
                switch (asInt)
                {
                    case 0:
                        _msg = "SUCCESS" + text;
                        _account.pushInit(this, SENDER_ID);
                        break;
                    case 1301:
                        _account.RecoverUser(this);
                        break;
                    case 1202:
                        _account.ResolveAlreadyLoginedUser(this);
                        break;
                    default:
                        _msg = "FAILED" + text;
                        break;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeLogout:
            case NPRequestTypeTag.NPRequestTypeUnregisterSVC:
                if (asInt == 0)
                {
                    _msg = "SUCCESS" + text;
                }
                else if (_account.isAuthCrashError(asInt))
                {
                    _msg = "AUTH CRASH" + text;
                }
                else
                {
                    _msg = "FAILED" + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeGetUserInfo:
                if (asInt == 0)
                {
                    _msg = string.Concat("SUCCESS npsn = ", npResult.resultJson["result"]["npsn"], " all json:", text);
                }
                else if (_account.isAuthCrashError(asInt))
                {
                    _msg = "AUTH CRASH" + text;
                }
                else
                {
                    _msg = "FAILED" + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeGetFriends:
                if (asInt == 0)
                {
                    _hasNext = npResult.resultJson["result"]["hasNext"].AsInt;
                    _msg = "SUCCESS" + text;
                }
                else if (_account.isAuthCrashError(asInt))
                {
                    _msg = "AUTH CRASH" + text;
                }
                else
                {
                    _msg = "FAILED" + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypePutCouponPin:
                _msg = "putcoupon " + text;
                break;
            case NPRequestTypeTag.NPRequestTypeSNSConnectWithFacebook:
                _msg = text;
                if (asInt == 0)
                {
                    _bFbConnect = true;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSConnectWithGPlus:
                _msg = text;
                if (asInt == 0)
                {
                    _bGpConnect = true;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSConnectWithTwitter:
                _msg = text;
                if (asInt == 0)
                {
                    _bTwConnect = true;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSDisconnectWithFacebook:
                _msg = text;
                if (asInt == 0)
                {
                    _bFbConnect = false;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSDisconnectWithGPlus:
                _msg = text;
                if (asInt == 0)
                {
                    _bGpConnect = false;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSDisconnectWithTwitter:
                _msg = text;
                if (asInt == 0)
                {
                    _bTwConnect = false;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeSNSGetUserInfoWithFacebook:
            case NPRequestTypeTag.NPRequestTypeSNSGetUserInfoWithTwitter:
            case NPRequestTypeTag.NPRequestTypeSNSGetUserInfoWithGPlus:
            case NPRequestTypeTag.NPRequestTypeGetTokenList:
                _msg = text;
                break;
            case NPRequestTypeTag.NPRequestTypeGetSnsConnectionStatus:
            {
                _msg = text;
                if (asInt != 0)
                {
                    break;
                }

                var jsonNode = npResult.resultJson["result"]["list"];
                for (var i = 0; i < jsonNode.Count; i++)
                {
                    var jsonNode2 = jsonNode[i];
                    if (jsonNode2["name"].Value == "facebook")
                    {
                        _bFbConnect = jsonNode2["isConnect"].AsInt == 1;
                    }
                    else if ((string) jsonNode2["name"] == "googleplus")
                    {
                        _bGpConnect = jsonNode2["isConnect"].AsInt == 1;
                    }
                    else if ((string) jsonNode2["name"] == "twitter")
                    {
                        _bTwConnect = jsonNode2["isConnect"].AsInt == 1;
                    }
                }

                break;
            }
            case NPRequestTypeTag.NPRequestTypeGetPromotion:
                if (asInt == 0)
                {
                    _account.ShowPromotion("placement_1", this);
                }
                else
                {
                    _msg = "getPromotion : " + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeShowPromotion:
                if (asInt == 0)
                {
                    _msg = "close";
                }
                else
                {
                    _msg = "showPromotion : " + text;
                }

                break;
            case NPRequestTypeTag.ConnectGamePlatform:
            case NPRequestTypeTag.DisconnectGamePlatform:
            case NPRequestTypeTag.LogoutGamePlatform:
                if (asInt == 0)
                {
                    _msg = "Success : " + text;
                }
                else
                {
                    _msg = "Fail : " + text;
                }

                break;
            case NPRequestTypeTag.NPRequestTypeGetAdId:
                if (asInt == 0)
                {
                    _msg = "Success adId = " + npResult.resultJson["result"]["adId"];
                }
                else
                {
                    _msg = "Fail : " + text;
                }

                break;
            default:
                _msg = text;
                break;
        }
    }

    public void OnRecvNotification(JSONNode recvNotification)
    {
        _msg = recvNotification.ToString();
    }

    public void OnEndingBannerClick(string landInfo)
    {
        _msg = "OnEndingBannerClick = " + landInfo;
        _account.DismissEndingBanner();
    }

    public void OnEndingBannerFailed(NPResult npResult)
    {
    }

    public void OnEndingBannerDismiss()
    {
    }

    public void OnEndingBannerExit()
    {
        Application.Quit();
    }

    public void OnBannerClick(string landInfo)
    {
        _msg = "OnBannerClick = " + landInfo;
    }

    public void OnBannerFailed(NPResult npResult)
    {
        _msg = npResult.resultJson.ToString();
    }

    public void OnBannerDismiss()
    {
        _msg = "OnBannerDismiss";
    }

    public void OnClose(NPCloseResult closeResult)
    {
        _msg = closeResult.screenName;
    }

    public void OnActionPerformedResult(NPResult npResult)
    {
        _msg = npResult.resultJson.ToString();
    }

    public void OnGCMResult(int errorCode)
    {
        _msg = "OnGCMResult = " + errorCode;
    }

    public void OnRequestPermissionsResult(int requestCode, string[] permissions, int[] grantResults)
    {
        _msg = string.Concat("requestCode = ", requestCode, " permissions", permissions, " grantResults", grantResults);
        foreach (var permission in permissions)
        {
            Debug.Log("permissions:" + permission);
        }

        foreach (var grantResult in grantResults)
        {
            Debug.Log("grantResults:" + grantResult);
        }
    }
}