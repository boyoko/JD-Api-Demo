using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.Common
{
    public class BaseResult<T>
    {
        public bool Success { get; set; }
        public string ResultMessage { get; set; }
        public string ResultCode { get; set; }
        public T Result { get; set; }
    }

    public class TokenResult
    {
        public string uid { get; set; }
        public long refresh_token_expires { get; set; }
        public long time { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
    }


    public class PageNumResult
    {
        public string name { get; set; }
        public string page_num { get; set; }
    }

    public class ProductDetailResult
    {
        public string saleUnit { get; set; }
        public string weight { get; set; }
        public string productArea { get; set; }
        public string wareQD { get; set; }
        public string imagePath { get; set; }
        public string param { get; set; }
        public int state { get; set; }
        public int sku { get; set; }
        public string shouhou { get; set; }
        public string brandName { get; set; }
        public string upc { get; set; }
        public string appintroduce { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string introduction { get; set; }

    }

    public class CategoryResult
    {
        public int catId { get; set; }
        public int parentId { get; set; }
        public string name { get; set; }
        public int catClass { get; set; }
        public int state { get; set; }
    }

}
