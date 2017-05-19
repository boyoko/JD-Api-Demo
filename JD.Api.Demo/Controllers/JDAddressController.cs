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
    [Route("api/JDAddress")]
    public class JDAddressController : Controller
    {
        private string GetActionName()
        {
            var controllerName = RouteData.Values["controller"] as string;
            var actionName = RouteData.Values["action"] as string;
            return "area/" + actionName;
        }

        [HttpGet("GetProvince")]
        public async Task<IActionResult> getProvince()
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 获取京东二级地址
        /// </summary>
        /// <param name="id">一级地址id</param>
        /// <returns></returns>
        [HttpGet("GetCity")]
        public async Task<IActionResult> getCity(string id)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("id", id);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// 获取京东三级地址
        /// </summary>
        /// <param name="id">二级地址id</param>
        /// <returns></returns>
        [HttpGet("GetCounty")]
        public async Task<IActionResult> getCounty(string id)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("id", id);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// 获取京东四级地址
        /// </summary>
        /// <param name="id">三级地址id</param>
        /// <returns></returns>
        [HttpGet("GetTown")]
        public async Task<IActionResult> getTown(string id)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("id", id);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 验证四级地址是否正确
        /// </summary>
        /// <param name="provinceId">一级地址id</param>
        /// <param name="cityId">二级地址id</param>
        /// <param name="countyId">三级地址，如果是空请传入0</param>
        /// <param name="townId">四级地址，如果是空请传入0</param>
        /// <returns></returns>
        [HttpGet("CheckArea")]
        public async Task<IActionResult> checkArea(string provinceId,string cityId,string countyId,string townId)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("provinceId", provinceId);
                dic.Add("cityId", cityId);
                dic.Add("countyId", countyId);
                dic.Add("townId", townId);
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