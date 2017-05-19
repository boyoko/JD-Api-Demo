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
        /// ������ȡ���ӿڣ����鶩������ҳ���µ�ʹ�ã�
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
        /// ������ȡ���ӿ�(������Ʒ�б�ҳʹ��)
        /// </summary>
        /// <param name="sku">��Ʒ��� �����Զ��ŷָ�  (���֧��100����Ʒ)</param>
        /// <param name="area">��ʽ��1_0_0 (�ֱ����1��2��3����ַ)</param>
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
        /// ������ȡ���ӿڣ�������ʹ�ã���5Ϊ��ֵ��
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