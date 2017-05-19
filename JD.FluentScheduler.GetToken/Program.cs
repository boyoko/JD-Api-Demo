using JD.NetCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.AutoRunService.GetToken
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //FluentScheduler.JobManager.AddJob(() =>
            //{
            //    var token = RefreshToken().GetAwaiter().GetResult();
            //    //var token = GetDetail().GetAwaiter().GetResult();

            //}, t =>
            //{
            //    t.ToRunNow().AndEvery(24).Hours();
            //});

            //GetMethodName();

            Console.Read();
        }

        //public static string GetMethodName(string message = "",
        //[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        //[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        //[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    Console.WriteLine(memberName);
        //    return memberName;
        //}
        /// <summary>
        /// 每天自动刷新token
        /// </summary>
        /// <returns></returns>
        private static async Task<string> RefreshToken()
        {
            var token = string.Empty;
            var refresh_token = await CacheHelper.Get(ConstHelper.REFRESH_TOKEN);
            if (!string.IsNullOrWhiteSpace(refresh_token))
            {
                var url = "https://bizapi.jd.com/oauth2/refreshToken";
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                var client_id = ConstHelper.CLIENT_ID;
                var client_secret = ConstHelper.CLIENT_SECRET;
                dic.Add("refresh_token", refresh_token);
                dic.Add("client_id", client_id);
                dic.Add("client_secret", client_secret);
                var x = await HttpHelper.HttpClientPost(url, dic);
                var tokenResult = StringHelper.JsonToObj<BaseResult<TokenResult>>(x)
                    as BaseResult<TokenResult>;

                if (tokenResult.Success)
                {
                    if(tokenResult.Result!=null && !string.IsNullOrWhiteSpace(tokenResult.Result.access_token))
                    {
                        await CacheHelper.Set(ConstHelper.ACCESS_TOKEN, tokenResult.Result.access_token);
                        await CacheHelper.Set(ConstHelper.REFRESH_TOKEN, tokenResult.Result.refresh_token);
                        token = tokenResult.Result.access_token;
                    }
                }
            }
            else
            {
                //缓存中没有refresh_token 时，先获取token，refresh_token 等信息，并进行缓存，
                token = await GetToken();
            }
            return token;
        }


        private static async Task<string> GetToken()
        {
            var token = await CacheHelper.Get(ConstHelper.ACCESS_TOKEN);
            if (string.IsNullOrWhiteSpace(token))
            {
                var url = "https://bizapi.jd.com/oauth2/accessToken";
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                var grant_type = ConstHelper.ACCESS_TOKEN;
                var client_id = ConstHelper.CLIENT_ID;
                var client_secret = ConstHelper.CLIENT_SECRET;
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var username = "门财科技";
                var password = EncryptHelper.MD5Hash("123456");
                var scope = string.Empty;
                var tmp = client_secret
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
                /*
                 {"success":true,"resultMessage":"","resultCode":"0000","result":{"uid":"0595365148","refresh_token_expires":1510121020030,"time":1494396220030,"expires_in":86400,"refresh_token":"dmF0bfZwcSkCaZiMTOtXhnuHnlILQGuDZjezsUNQ","access_token":"cvUF16ni3EBujqYsdzzWikKza"}}
                 */
                var x = await HttpHelper.HttpClientPost(url, dic);

                var tokenResult = StringHelper.JsonToObj<BaseResult<TokenResult>>(x)
                    as BaseResult<TokenResult>;

                if (tokenResult.Success)
                {
                    if (tokenResult.Result != null && !string.IsNullOrWhiteSpace(tokenResult.Result.access_token))
                    {
                        await CacheHelper.Set(ConstHelper.ACCESS_TOKEN, tokenResult.Result.access_token);
                        await CacheHelper.Set(ConstHelper.REFRESH_TOKEN, tokenResult.Result.refresh_token);
                        token = tokenResult.Result.access_token;
                    }
                }

            }
            return token;
        }


        private static async Task<bool> GetDetail()
        {
            var skuStr = await CacheHelper.Get("skuList-672");
            var list = skuStr.Split(',').ToList();
            foreach(var sku in list)
            {
                var x = await HttpHelper.HttpClientGet("",sku);
            }
            return true;
        }

    }
}