using System.Threading.Tasks;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace WebWechat.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    public partial class CommonApi
    {
        #region 同步方法

        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfo(string accessTokenOrAppId, string openId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                        accessToken.AsUrlData(), openId.AsUrlData());
                WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
                return result;

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string appId, string secret, string type = "jsapi")
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            return GetTicketByAccessToken(accessToken, type);
        }

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketByAccessToken(string accessTokenOrAppId, string type = "jsapi")
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
                return result;

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static GetCallBackIpResult GetCallBackIp(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<GetCallBackIpResult>(url);

            }, accessTokenOrAppId);
        }

        #endregion
#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static async Task<AccessTokenResult> GetTokenAsync(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = await Get.GetJsonAsync<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 【异步方法】用户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static async Task<WeixinUserInfoResult> GetUserInfoAsync(string accessTokenOrAppId, string openId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                        accessToken.AsUrlData(), openId.AsUrlData());
                var result = Get.GetJsonAsync<WeixinUserInfoResult>(url);
                return await result;

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketAsync(string appId, string secret, string type = "jsapi")
        {
            var accessToken = await AccessTokenContainer.TryGetAccessTokenAsync(appId, secret);
            return GetTicketByAccessToken(accessToken, type);
        }

        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketByAccessTokenAsync(string accessTokenOrAppId, string type = "jsapi")
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                var result = Get.GetJsonAsync<JsApiTicketResult>(url);
                return await result;

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static async Task<GetCallBackIpResult> GetCallBackIpAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return await Get.GetJsonAsync<GetCallBackIpResult>(url);

            }, accessTokenOrAppId);
        }

        #endregion
#endif

    }
}
