using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using JD.NetCore.Common;

namespace JD.Api.Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/JDPrice")]
    public class JDPriceController : Controller
    {
        private string GetActionName()
        {
            //var controllerName = RouteData.Values["controller"] as string;
            var actionName = RouteData.Values["action"] as string;
            return "price/" + actionName;
        }

        /// <summary>
        /// 批量查询京东价价格
        /// </summary>
        /// <param name="skus">商品编号，请以，分割。例如：J_129408,J_129409(最高支持100个商品)</param>
        /// <returns></returns>
        [HttpGet("GetJdPrice")]
        public async Task<IActionResult> getJdPrice(string skus)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("sku", skus);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// <summary>
        /// 批量查询协议价价格
        /// </summary>
        /// <param name="skus">商品编号，请以，分割。例如：J_129408,J_129409(最高支持100个商品)</param>
        /// <returns></returns>
        [HttpGet("GetPrice")]
        public async Task<IActionResult> getPrice(string skus)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("sku", skus);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// 批量查询商品售卖价
        /// </summary>
        /// <param name="skus">商品编号，请以，分割。例如：J_129408,J_129409(最高支持100个商品)</param>
        /// <returns></returns>
        [HttpGet("GetSellPrice")]
        public async Task<IActionResult> getSellPrice(string skus)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("sku", skus);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}