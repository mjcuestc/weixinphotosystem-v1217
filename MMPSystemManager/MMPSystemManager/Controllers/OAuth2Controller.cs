using Microsoft.AspNetCore.Mvc;
using MMPSystemManager.DBContext;
using MMPSystemManager.Module;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;



namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class OAuth2Controller : Controller
    {
        public string appId = "wxc222968bebebe075"; //测试号appid
        public string secret = "da6f5c0b6c4382c39fc442b267f7ba3a"; //测试号APPSECRET
        public string redirectUrl = "http://www.qingshuihe.xyz/OAuth2/UserAccessToken"; //回调地址,通常为需要授权业务网页（域名与微信后台一致）
        public string returnUrl = string.Empty; //存储向微信提交申请地址（获得code）;
        //public string Session = string.Empty;
        OAuthAccessTokenResult accesstoken = null; //存储code换取access_token返回的JSON数据
        OAuthUserInfo usermessage = null; //存储openid access_token换取用户JSON数据

        private readonly MMPContext _context;

        public OAuth2Controller(MMPContext context)
        {
            _context = context;
        }
        ///换取code
        public ActionResult Index()
        {
            //var state = "JeffreySu-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性；
            //Session["State"] = state;//参数State随机填；
            returnUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=123#wechat_redirect", 
                appId, redirectUrl.UrlEncode());
            
            return Redirect(returnUrl);
        }

        ///获取access_toker
        ///获取用户信息;
        ///<param name="code"></param>
        ///<param name="state"></param>
        ///<param name="returnUrl"></param>
        public ActionResult UserAccessToken(string code, string state )
        {
            //获取用户access_token;
            if(string.IsNullOrEmpty(code))
            {
                return Redirect("http://www.qingshuihe.xyz/OAuth2/ActionResult1"); //若没有授权，跳转到snsapi_base方式访问
            }
            /*if(state != Session["State"] as string) //再进一步验证初试随机设置state是否一致
            {
                return Content("验证失败!");
            }
            */
            try
            {
                accesstoken = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }

            if (accesstoken.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + accesstoken.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            //Session["OAuthAccessToken"] = accesstoken;

            //判断access_token是否有效，刷新access_token
            WxJsonResult result = OAuthApi.Auth(accesstoken.access_token, accesstoken.openid);
            if(result.errcode == ReturnCode.access_token超时)
            {
                accesstoken = OAuthApi.RefreshToken(appId, accesstoken.access_token);
            }

            //获取用户信息
            try
            {
                usermessage = OAuthApi.GetUserInfo(accesstoken.access_token, accesstoken.openid);
            }
            catch
            {
                return Content("GetUserInfo error!");
            }

            ViewBag.user_openid = usermessage.openid;
            OpenIDPra.UserOpenID = usermessage.openid;
            /*            ViewBag.nickname = usermessage.nickname;
                        ViewBag.province = usermessage.province;
                        ViewBag.sex = usermessage.sex;
                        ViewBag.city = usermessage.city;
                        ViewBag.country = usermessage.country;
                        ViewBag.headimgurl = usermessage.headimgurl;
                        ViewBag.privilege = usermessage.privilege;
                        ViewBag.user_unionid = usermessage.unionid;*/
            return Redirect("http://www.qingshuihe.xyz/Reservation/reser.html");

        }

        /// <summary>
        /// 测试snsapi_base
        /// </summary>
        /// 只能拿到用户的openid(唯一)
        /// <returns></returns>
        string base_returnUrl = string.Empty; 
        string base_redirectUrl = "http://www.qingshuihe.xyz/OAuth2/UserAccessToken"; //重定向url（通常为业务网页）
        public ActionResult ActionResult1()
        {
            //var state = "JeffreySu-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性；
            //Session["State"] = state;//参数State随机填；

            base_returnUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=123#wechat_redirect",
                appId, base_redirectUrl.UrlEncode());
            
            return Redirect(base_returnUrl);
        }

    }
}