using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using JD.NetCore.Common;
using JD.Api.Demo.Models;

namespace JD.Api.Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/JDStock")]
    public class JDStockController : Controller
    {
        private string GetActionName()
        {
            //var controllerName = RouteData.Values["controller"] as string;
            var actionName = RouteData.Values["action"] as string;
            return "stock/" + actionName;
        }
        /// <summary>
        /// 批量获取库存接口（建议订单详情页、下单使用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetNewStockById")]
        public async Task<IActionResult> getNewStockById([FromBody] GetNewStockByIdRequestDto request)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("skuNums", StringHelper.ToJson(request.skuNums));
                dic.Add("area", request.area);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// 批量获取库存接口(建议商品列表页使用)
        /// </summary>
        /// <param name="sku">商品编号 批量以逗号分隔  (最高支持100个商品)</param>
        /// <param name="area">格式：1_0_0 (分别代表1、2、3级地址)</param>
        /// <returns></returns>
        [HttpGet("GetStockById")]
        public async Task<IActionResult> getStockById(string sku,string area)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("sku", sku);
                dic.Add("area", area);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        /// <summary>
        /// 批量获取库存接口（买卖宝使用，以5为阈值）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetFiveStockById")]
        public async Task<IActionResult> getFiveStockById([FromBody] GetNewStockByIdRequestDto request)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("skuNums", StringHelper.ToJson(request.skuNums));
                dic.Add("area", request.area);
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