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
        /// ��ȡ����������ַ
        /// </summary>
        /// <param name="id">һ����ַid</param>
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
        /// ��ȡ����������ַ
        /// </summary>
        /// <param name="id">������ַid</param>
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
        /// ��ȡ�����ļ���ַ
        /// </summary>
        /// <param name="id">������ַid</param>
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
        /// ��֤�ļ���ַ�Ƿ���ȷ
        /// </summary>
        /// <param name="provinceId">һ����ַid</param>
        /// <param name="cityId">������ַid</param>
        /// <param name="countyId">������ַ������ǿ��봫��0</param>
        /// <param name="townId">�ļ���ַ������ǿ��봫��0</param>
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