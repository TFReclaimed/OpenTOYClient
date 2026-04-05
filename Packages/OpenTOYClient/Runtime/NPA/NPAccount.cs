using System.Collections.Generic;
using Newtonsoft.Json;
using NPA.TOY;
using NPA.TOY.Request;
using SimpleJSON;
using UnityEngine;

namespace NPA
{
    public class NPAccount
    {
        private class Nested
        {
            internal static readonly NPAccount instance;

            static Nested()
            {
                instance = new NPAccount();
            }
        }

        public static string FRIEND_FILTER_TYPE_FRIENDS = "friends";

        public static string FRIEND_FILTER_TYPE_INVITES = "invites";

        public static string FRIEND_FILTER_TYPE_ALL = string.Empty;

        public static int LOCAL_PUSH_TYPE_ON;

        public static int LOCAL_PUSH_TYPE_AFTER = 1;

        public static int LOCAL_PUSH_TYPE_NOW = 2;

        public string GAMEOBJECT_NAME = "NPAccount";

        public NPAccountGameObject mGameObject;

        public static string serviceID;

        public static NPAccount Instance => Nested.instance;

        private readonly ToySession _session = new();

        private NPAccount()
        {
            mGameObject = new GameObject(GAMEOBJECT_NAME).AddComponent<NPAccountGameObject>();
            Object.DontDestroyOnLoad(mGameObject);

            ToyRequestFactory.ToyInfo = Resources.Load<ToyInfoObject>("ToyInfo");
            if (ToyRequestFactory.ToyInfo == null)
            {
                Debug.LogError("[TOY] ToyInfo asset not found in Resources folder. Please create a ToyInfo asset.");
            }
            else
            {
                _session.ServiceId = ToyRequestFactory.ToyInfo.ServiceId;
            }
        }

        public bool isAuthCrashError(int errorCode)
        {
            return errorCode switch
            {
                5001 or 5002 or 5003 or 90502 or 90707 => true,
                _ => false
            };
        }

        public void pushInit(INPRecvNotificationListener pushListener, string senderID = "")
        {
            ToyDebugLog("pushInit - unimplemented");
        }

        public void pushInit(INPRecvNotificationListener pushListener, INPGCMListener gcmListener, bool isSkip,
            string senderID = "")
        {
            ToyDebugLog("pushInit - unimplemented");
        }

        public void RegisterPush(INPListener listener)
        {
            ToyDebugLog("RegisterPush - unimplemented");
        }

        public void UnregisterPush(INPListener listener)
        {
            ToyDebugLog("UnregisterPush - unimplemented");
        }

        public void Login(INPListener listener)
        {
            ToyDebugLog("Login - unimplemented");
        }

        public void Login(NPLoginType loginType, INPListener listener)
        {
            ToyDebugLog("Login - unimplemented");
        }

        public void LoginForKakao(string kakaoID, string accessToken, INPListener listener)
        {
            ToyDebugLog("LoginForKakao - unimplemented");
        }

        public void UnregisterService(INPListener listener)
        {
            ToyDebugLog("UnregisterService - unimplemented");
        }

        public void RecoverUser(INPListener listener)
        {
            ToyDebugLog("RecoverUser - unimplemented");
        }

        public void Logout(INPListener listener)
        {
            ToyDebugLog("Logout - unimplemented");
        }

        public void GetUserInfo(INPListener listener)
        {
            var request = (ToyGetUserInfoRequest) ToyRequestFactory.CreateRequest(ToyRequestType.GetUserInfo, _session);

            request.SetListener(result =>
            {
                listener.OnResult(new NPResult
                {
                    requestTag = NPRequestTypeTag.NPRequestTypeGetUserInfo,
                    errorCode = result.errorCode,
                    resultJson = JSONNode.Parse(JsonConvert.SerializeObject(result))
                });
            });

            mGameObject.ExecuteRequest(request);
        }

        public void GetFriends(int next, string filterType, INPListener listener)
        {
            ToyDebugLog("GetFriends - unimplemented");
        }

        public void GetFriends(NPSnsType snsType, int next, string filterType, INPListener listener)
        {
            ToyDebugLog("GetFriends - unimplemented");
        }

        public void Share(string title, string content, string url)
        {
            ToyDebugLog("Share - unimplemented");
        }

        public void ShowBanner(string groupCode, INPBannerListener bannerListener)
        {
            ToyDebugLog("ShowBanner - unimplemented");
        }

        public void NXLogin(string id, string pw, INPListener listener)
        {
            ToyDebugLog("NXLogin - unimplemented");
        }

        public void Post(string msg, string description, string link, INPListener listener)
        {
            ToyDebugLog("Post - unimplemented");
        }

        public void PostImage(string msg, string imageURL, INPListener listener)
        {
            ToyDebugLog("PostImage - unimplemented");
        }

        public void DismissEndingBanner()
        {
            ToyDebugLog("DismissEndingBanner - unimplemented");
        }

        public void GetNexonSN(INPListener listener)
        {
            ToyDebugLog("GetNexonSN - unimplemented");
        }

        public NPLoginType GetLoginType()
        {
            ToyDebugLog("GetLoginType - unimplemented");
            return NPLoginType.NPLoginTypeDefault;
        }

        public void GetLoginType(INPListener listener)
        {
            ToyDebugLog("GetLoginType - unimplemented");
        }

        public void ShowNotice()
        {
            ToyDebugLog("ShowNotice - unimplemented");
        }

        public void ShowNotice(INPOnCloseListener closeListener)
        {
            ToyDebugLog("ShowNotice - unimplemented");
        }

        public void ShowFAQ()
        {
            ToyDebugLog("ShowFAQ - unimplemented");
        }

        public void ShowWeb(string title, string url)
        {
            ToyDebugLog("ShowWeb - unimplemented");
        }

        public void ShowWeb(string title, string url, string postData)
        {
            ToyDebugLog("ShowWeb - unimplemented");
        }

        public void ShowEventWeb(string url)
        {
            ToyDebugLog("ShowEventWeb - unimplemented");
        }

        public void ShowEndingBanner(INPEndingBannerListener endingBannerListener)
        {
            ToyDebugLog("ShowEndingBanner - unimplemented");
        }

        public void ShowInputCoupon(INPListener listener)
        {
            ToyDebugLog("ShowInputCoupon - unimplemented");
        }

        public void ShowCustomerService(NPCSInfo param)
        {
            ToyDebugLog("ShowCustomerService - unimplemented");
        }

        public void ShowCustomerService(NPCSInfo param, INPPlateListener plateListener)
        {
            ShowPlate(0, param, plateListener);
        }

        public void ShowHelpCenter(NPCSInfo param)
        {
            ToyDebugLog("ShowHelpCenter - unimplemented");
        }

        public void SendEvent(string category, string action, string label, string value)
        {
            ToyDebugLog("SendEvent - unimplemented");
        }

        public void SendScreen(string screenName)
        {
            ToyDebugLog("SendScreen - unimplemented");
        }

        public void SendEcommerceTransaction(string transactionID, string affiliation, double revenue, double tax,
            double shipping, string currencyCode)
        {
            ToyDebugLog("SendEcommerceTransaction - unimplemented");
        }

        public void SendEcommerceItem(string transactionID, string name, string sky, string category, double price,
            long quantity, string currencyCode)
        {
            ToyDebugLog("SendEcommerceItem - unimplemented");
        }

        public void StartSession()
        {
            ToyDebugLog("StartSession - unimplemented");
        }

        public void EndSession()
        {
            ToyDebugLog("EndSession - unimplemented");
        }

        public void setCountry(NPCountry country)
        {
            ToyDebugLog("setCountry - unimplemented");
        }

        public NPCountry getCountry()
        {
            ToyDebugLog("getCountry - unimplemented");
            return NPCountry.UnitedStates;
        }

        public void setLocale(NPLocale locale)
        {
            ToyDebugLog("setLocale - unimplemented");
        }

        public NPLocale getLocale()
        {
            ToyDebugLog("getLocale - unimplemented");
            return NPLocale.EN_US;
        }

        public void dispatchLocalPush(NPNotificationData data)
        {
            ToyDebugLog("dispatchLocalPush - unimplemented");
        }

        public void cancelLocalPush(int notificationID)
        {
            ToyDebugLog("cancelLocalPush - unimplemented");
        }

        public void cancelAllLocalPush()
        {
            ToyDebugLog("cancelAllLocalPush - unimplemented");
        }

        public void showAchievement(INPListener gameServiceCloseListener)
        {
            ToyDebugLog("showAchievement - unimplemented");
        }

        public void setStepsAchievement(string achievementID, int steps)
        {
            ToyDebugLog("setStepsAchievement - unimplemented");
        }

        public void setStepsAchievementImmediate(string achievementID, int steps, INPListener listener)
        {
            ToyDebugLog("setStepsAchievementImmediate - unimplemented");
        }

        public void unlockAchievement(string achievementID)
        {
            ToyDebugLog("unlockAchievement - unimplemented");
        }

        public void unlockAchievementImmediate(string achievementID, INPListener listener)
        {
            ToyDebugLog("unlockAchievementImmediate - unimplemented");
        }

        public void incrementAchievement(string achievementID, int increment)
        {
            ToyDebugLog("incrementAchievement - unimplemented");
        }

        public void incrementAchievementImmediate(string achievementID, int increment, INPListener listener)
        {
            ToyDebugLog("incrementAchievementImmediate - unimplemented");
        }

        public void loadAchievementData(bool forceReload, INPListener listener)
        {
            ToyDebugLog("loadAchievementData - unimplemented");
        }

        public void showAllLeaderBoard(INPListener gameServiceCloseListener)
        {
            ToyDebugLog("showAllLeaderBoard - unimplemented");
        }

        public void showLeaderBoard(string leaderBoardID, INPListener gameServiceCloseListener)
        {
            ToyDebugLog("showLeaderBoard - unimplemented");
        }

        public void submitScore(string leaderBoardID, long score)
        {
            ToyDebugLog("submitScore - unimplemented");
        }

        public void submitScoreImmediate(string leaderBoardID, long score, INPListener listener)
        {
            ToyDebugLog("submitScoreImmediate - unimplemented");
        }

        public void loadCurrentPlayerLeaderboardScore(string leaderBoardID, int span, int leaderboardCollection,
            INPListener listener)
        {
            ToyDebugLog("loadCurrentPlayerLeaderboardScore - unimplemented");
        }

        public void ConnectGamePlatform(INPListener listener)
        {
            ToyDebugLog("ConnectGamePlatform - unimplemented");
        }

        public void DisconnectGamePlatform(INPListener listener)
        {
            ToyDebugLog("DisconnectGamePlatform - unimplemented");
        }

        public void LogoutGamePlatform(INPListener listener)
        {
            ToyDebugLog("LogoutGamePlatform - unimplemented");
        }

        public bool IsEnableGamePlatform()
        {
            ToyDebugLog("IsEnableGamePlatform - unimplemented");
            return false;
        }

        public void ScreenCapture(INPListener listener)
        {
            ToyDebugLog("ScreenCapture - unimplemented");
        }

        public void GetPlayerStats(bool forceReload, INPListener listener)
        {
            ToyDebugLog("GetPlayerStats - unimplemented");
        }

        public void ShowPlate(NPCSInfo param)
        {
            ShowPlate(0, param, null);
        }

        public void ShowPlate(int group, NPCSInfo param)
        {
            ShowPlate(group, param, null);
        }

        public void ShowPlate(int group, NPCSInfo param, INPPlateListener plateListener)
        {
            ToyDebugLog("ShowPlate - unimplemented");
        }

        public void ShowTermsOfAgree(INPListener listener)
        {
            ToyDebugLog("ShowTermsOfAgree - unimplemented");
        }

        public void snsConnect(NPSnsType snsType, INPListener listener)
        {
            ToyDebugLog("snsConnect - unimplemented");
        }

        public void snsDisconnect(NPSnsType snsType, INPListener listener)
        {
            ToyDebugLog("snsDisconnect - unimplemented");
        }

        public void getSnsConnectionStatus(INPListener listener)
        {
            ToyDebugLog("getSnsConnectionStatus - unimplemented");
        }

        public void getSnsUserInfo(NPSnsType snsType, INPListener listener)
        {
            ToyDebugLog("getSnsUserInfo - unimplemented");
        }

        public void getSnsTokenList(INPListener listener)
        {
            ToyDebugLog("getSnsTokenList - unimplemented");
        }

        public void GetCountryFromServer(INPListener listener)
        {
            ToyDebugLog("GetCountryFromServer - unimplemented");
        }

        public string getUUID()
        {
            ToyDebugLog("getUUID - unimplemented");
            return string.Empty;
        }

        public void ShowDataBackup(INPListener listener)
        {
            ShowDataBackup(string.Empty, listener);
        }

        public void ShowDataBackup(string title, INPListener listener)
        {
            ToyDebugLog("ShowDataBackup - unimplemented");
        }

        public void ShowDataRestore(INPListener listener)
        {
            ShowDataRestore(string.Empty, listener);
        }

        public void ShowDataRestore(string title, INPListener listener)
        {
            ToyDebugLog("ShowDataRestore - unimplemented");
        }

        public void SetDisableLoginTypes(int[] loginTypes)
        {
            ToyDebugLog("SetDisableLoginTypes - unimplemented");
        }

        public void ResolveAlreadyLoginedUser(INPListener listener)
        {
            ToyDebugLog("ResolveAlreadyLoginedUser - unimplemented");
        }

        public void EnterToy(INPListener listener)
        {
            var request = (ToyEnterRequest) ToyRequestFactory.CreateRequest(ToyRequestType.EnterToy, _session);

            request.SetListener(result =>
            {
                listener.OnResult(new NPResult
                {
                    requestTag = NPRequestTypeTag.NPRequestTypeEnterToy,
                    errorCode = result.errorCode,
                    resultJson = JSONNode.Parse(JsonConvert.SerializeObject(result))
                });
            });

            mGameObject.ExecuteRequest(request);
        }

        public void ShowSettlementFund(string itemName, string itemPrice, INPListener listener)
        {
            ToyDebugLog("ShowSettlementFund - unimplemented");
        }

        public void ShowPushNSms(INPListener listener)
        {
            ToyDebugLog("ShowPushNSms - unimplemented");
        }

        public void GetServiceInfo(INPListener listener)
        {
            ToyDebugLog("GetServiceInfo - unimplemented");
        }

        public void GetAdvertisingId(INPListener listener)
        {
            ToyDebugLog("GetAdvertisingId - unimplemented");
        }

        public void FBLogPurchase(double purchaseAmount, string currency, Dictionary<string, string> param)
        {
            ToyDebugLog("FBLogPurchase - unimplemented");
        }

        public void FBLogEvent(string eventName)
        {
            ToyDebugLog("FBLogEvent - unimplemented");
        }

        public void FBLogEvent(string eventName, Dictionary<string, string> param)
        {
            ToyDebugLog("FBLogEvent - unimplemented");
        }

        public void FBLogEvent(string eventName, double valueToSum)
        {
            ToyDebugLog("FBLogEvent - unimplemented");
        }

        public void FBLogEvent(string eventName, double valueToSum, Dictionary<string, string> param)
        {
            ToyDebugLog("FBLogEvent - unimplemented");
        }

        public void FBActivateApp()
        {
            ToyDebugLog("FBActivateApp - unimplemented");
        }

        public void FBDeactivateApp()
        {
            ToyDebugLog("FBDeactivateApp - unimplemented");
        }

        public void FBFetchDeferredAppLink(INPListener listener)
        {
            ToyDebugLog("FBFetchDeferredAppLink - unimplemented");
        }

        public void FBAppInvite(string appLinkUrl, string previewImageUrl, INPListener listener)
        {
            ToyDebugLog("FBAppInvite - unimplemented");
        }

        public void FBShare(string title, string description, string contentUrl, string imageUrl, INPListener listener)
        {
            ToyDebugLog("FBShare - unimplemented");
        }

        public void FBGetFriends(int nextPage, INPListener listener)
        {
            ToyDebugLog("FBGetFriends - unimplemented");
        }

        public void FBSetIsDebugEnabled(bool enabled)
        {
            ToyDebugLog("FBSetIsDebugEnabled - unimplemented");
        }

        public void ResetBadgeCount(int count)
        {
            ToyDebugLog("ResetBadgeCount - unimplemented");
        }

        public void SetPushAgreement(NPPushAgreementType agreeStatus, INPListener listener)
        {
            ToyDebugLog("SetPushAgreement - unimplemented");
        }

        public void RequestPermissions(List<string> permissions, int requestCode, string rationaleMsg,
            INPRuntimePermissionListener listener)
        {
            ToyDebugLog("RequestPermissions - unimplemented");
        }

        public void GetPromotion(string placementId, INPListener listener)
        {
            ToyDebugLog("GetPromotion - unimplemented");
        }

        public void ShowPromotion(string placementId, INPListener listener)
        {
            ToyDebugLog("ShowPromotion - unimplemented");
        }

        public static void ToyDebugLog(string text)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD || TOY_DEBUG
            Debug.Log($"[TOY] {text}");
#endif
        }
    }
}