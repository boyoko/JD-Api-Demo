using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JD.NetCore.Common;
using System.IO;

namespace JD.Api.Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {

        //private string GetControllerName()
        //{
        //    var controllerName = RouteData.Values["controller"] as string;
        //    return controllerName;
        //}

        private string GetActionName()
        {
            var controllerName = RouteData.Values["controller"] as string;
            var actionName = RouteData.Values["action"] as string;
            return controllerName.ToLower() + "/" + actionName;
        }

        [HttpGet("GetPageNum")]
        public async Task<IActionResult> getPageNum()
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// ���ӱ��ΪgetPageNum �ӿڷ��ص�ֵ
        /// �磺"name":"�ʼǱ�","page_num":"672"
        /// </summary>
        /// <param name="pageNum">���ӱ��</param>
        /// <returns>��Ʒ�������</returns>
        [HttpGet("GetSku")]
        public async Task<IActionResult> getSku(string pageNum)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("pageNum", pageNum);
                var x = await HttpHelper.HttpClientPost(url, dic);

                var skuResult = StringHelper.JsonToObj<BaseResult<string>>(x)
                    as BaseResult<string>;

                if (skuResult.Success)
                {
                    if (!string.IsNullOrWhiteSpace(skuResult.Result))
                    {
                        var jsonStr = skuResult.Result;
                        await CacheHelper.Set("skuList-"+ pageNum, jsonStr);
                    }
                }


                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ���ӱ��ΪgetPageNum �ӿڷ��ص�ֵ
        /// �磺"name":"�ʼǱ�","page_num":"672"
        /// </summary>
        /// <param name="pageNum">���ӱ��</param>
        /// <param name="pageNo">ҳ�룬Ĭ��ȡ��һҳ��ÿҳ���10000�����ݣ�Ʒ����Ʒ�ؿ��ܴ��ڶ�ҳ���ݣ�������ݷ��ص�ҳ�����ж��Ƿ�����һҳ����</param>
        /// <returns></returns>
        [HttpGet("GetSkuByPage")]
        public async Task<IActionResult> getSkuByPage(string pageNum,string pageNo)
        {
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("pageNum", pageNum);
                dic.Add("pageNo", pageNo);
                var x = await HttpHelper.HttpClientPost(url, dic);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku">��Ʒ���</param>
        /// <param name="isShow">false:��ѯ��Ʒ������Ϣ | true:��Ʒ������Ϣ + ��Ʒ�ۺ���Ϣ + �ƶ���Ʒ���������Ϣ</param>
        /// <returns></returns>
        [HttpGet("GetDetail")]
        public async Task<IActionResult> getDetail(string sku,bool isShow=false)
        {
            //var sku = "4738276";  //�ʼǱ�
            //var isShow = "true";
            ProductDetailResult t = null;
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("sku", sku);
                dic.Add("isShow", isShow.ToString());
                var x = await HttpHelper.HttpClientPost(url, dic);

                var productResult = StringHelper.JsonToObj<BaseResult<ProductDetailResult>>(x)
                    as BaseResult<ProductDetailResult>;

                if (productResult.Success)
                {
                    if (productResult.Result != null)
                    {
                        t = productResult.Result;
                    }
                }
                //var a = NestHelper.GetInstance("product")
                //    .DocumentExists<ProductDetailResult>(t,s=>s.Index("product").SourceExclude("")).Exists;

                var b = NestHelper.GetInstance("product")
                    .Index<ProductDetailResult>(t);

                if(b.Result== Nest.Result.Created && b.Created)
                {

                }
                else
                {
                    return BadRequest(b.OriginalException.Message);
                }

                return Ok(t);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("SearchFromES")]
        public async Task<IActionResult> SearchFromES(string key)
        {
            var x  =  NestHelper.GetInstance("product");
            var searchResponse = await x.SearchAsync<ProductDetailResult>(s => s
                            .From(0)
                            .Size(10)
                            .Query(q => q
                                 .Match(m => m
                                    .Field(f => f.name)
                                    //.Query("ѩ��1")
                                    .Query(key)
                                 )
                            )
                        );

            var product = searchResponse.Documents;
            return Ok(product);
        }


        [HttpGet("GetCategory")]
        public async Task<IActionResult> getCategory(string cid)
        {
            CategoryResult t = null;
            try
            {
                var url = Path.Combine(ConstHelper.BASEURL, GetActionName());
                var token = await CacheHelper.GetToken();
                IDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("token", token);
                dic.Add("cid", cid);
                var x = await HttpHelper.HttpClientPost(url, dic);

                var categoryResult = StringHelper.JsonToObj<BaseResult<CategoryResult>>(x)
                    as BaseResult<CategoryResult>;

                if (categoryResult.Success)
                {
                    if (categoryResult.Result != null)
                    {
                        t = categoryResult.Result;
                    }
                }
                return Ok(t);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        



    }
}