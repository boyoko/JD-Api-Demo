using JD.NetCore.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JD.Api.Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    public class JDApiController : Controller
    {
        public async Task<IActionResult> GetToken()
        {
            var url = "https://bizapi.jd.com/oauth2/accessToken";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            var grant_type = ConstHelper.ACCESS_TOKEN;
            var client_id = ConstHelper.CLIENT_SECRET;
            var client_secret = ConstHelper.CLIENT_SECRET;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var username = "门财科技";
            var password = EncryptHelper.MD5Hash("123456");
            var scope = string.Empty;
            var tmp =  client_secret
                        + timestamp
                        + client_id
                        + username
                        + password
                        + grant_type
                        + scope
                        + client_secret;
            var md5Tmp = EncryptHelper.MD5Hash(tmp.Trim());
            var upperStr = md5Tmp.ToUpper();
            var sign = upperStr;
            dic.Add("grant_type", grant_type);
            dic.Add("client_id", client_id);
            dic.Add("client_secret", client_secret);
            dic.Add("timestamp", timestamp);
            dic.Add("username", username);
            dic.Add("password", password);
            dic.Add("scope", scope);
            dic.Add("sign", sign);

            var x = await HttpHelper.HttpClientPost(url,dic);

            return new ObjectResult(x);
        }



        public async Task<IActionResult> RefreshToken()
        {
            var url = "https://bizapi.jd.com/oauth2/refreshToken";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            var refresh_token = await CacheHelper.Get(ConstHelper.REFRESH_TOKEN);
            if (string.IsNullOrWhiteSpace(refresh_token))
            {
                return new ObjectResult(new { Success = false, ErrorMessage= "从缓存获取refresh_token失败！" });
            }
            var client_id = ConstHelper.CLIENT_ID;
            var client_secret = ConstHelper.CLIENT_SECRET;
            dic.Add("refresh_token", refresh_token);
            dic.Add("client_id", client_id);
            dic.Add("client_secret", client_secret);
            var x = await HttpHelper.HttpClientPost(url, dic);
            return new ObjectResult(x);
        }



        public async Task<IActionResult> GetPageNum()
        {
            //J7Qp6Mh5vzndBBEKKbOxJ7s4j
            var url = "https://bizapi.jd.com/api/product/getPageNum";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            //var token = "J7Qp6Mh5vzndBBEKKbOxJ7s4j";
            var token = "hvfzs4e7FECZu4AOnXv9AQWoD";
            dic.Add("token", token);
            var x =await HttpHelper.HttpClientPost(url,dic);
            return new ObjectResult(x);
        }

        public async Task<IActionResult> GetSku()
        {
            //J7Qp6Mh5vzndBBEKKbOxJ7s4j
            var url = "https://bizapi.jd.com/api/product/getSku";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            var token = "J7Qp6Mh5vzndBBEKKbOxJ7s4j";
            var pageNum = "672";  //笔记本
            dic.Add("token", token);
            dic.Add("pageNum", pageNum);
            var x = await HttpHelper.HttpClientPost(url, dic);
            return new ObjectResult(x);
        }

        public async Task<IActionResult> GetSkuByPage()
        {
            //J7Qp6Mh5vzndBBEKKbOxJ7s4j
            var url = "https://bizapi.jd.com/api/product/getSkuByPage";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            var token = "J7Qp6Mh5vzndBBEKKbOxJ7s4j";
            var pageNum = "672";  //笔记本
            var pageNo = "1";
            dic.Add("token", token);
            dic.Add("pageNum", pageNum);
            dic.Add("pageNo", pageNo);
            var x = await HttpHelper.HttpClientPost(url, dic);
            return new ObjectResult(x);
        }


        public async Task<IActionResult> GetDetail()
        {
            //J7Qp6Mh5vzndBBEKKbOxJ7s4j
            var url = "https://bizapi.jd.com/api/product/getDetail";
            IDictionary<string, string> dic = new SortedDictionary<string, string>();
            var token = "J7Qp6Mh5vzndBBEKKbOxJ7s4j";
            var sku = "4738276";  //笔记本
            var isShow = "true";
            dic.Add("token", token);
            dic.Add("sku", sku);
            dic.Add("isShow", isShow);
            var x = await HttpHelper.HttpClientPost(url, dic);
            return new ObjectResult(x);
        }

    }
}