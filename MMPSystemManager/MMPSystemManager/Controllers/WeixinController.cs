using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MMPSystemManager.CommonAPIs;

namespace MMPSystemManager.Controllers
{
    public class WeixinController : Controller
    {
        public static readonly string Token = "mengmengpai";//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "IhGTgeJjdB2hqdFYXy6sq8T2NgAyegvg7LiDFwRo3Ya";//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = "wxc222968bebebe075";//与微信公众账号后台的AppId设置保持一致，区分大小写。

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        [HttpPost]
        [ActionName("MiniPost")]
        public ActionResult MiniPost(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }


            var messageHandler = new CustomMessageHandler(Request.Body, postModel);

            messageHandler.Execute();//执行微信处理过程
            //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
            //return new WeixinResult(messageHandler);//v0.8+
            return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
        }
    }
}