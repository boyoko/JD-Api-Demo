using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JD.Api.Demo.Models
{
    public class GetNewStockByIdRequestDto
    {
        //public GetNewStockByIdRequestDto()
        //{
        //    skuNums = new List<SkuAndNum>();
        //}
        public List<SkuAndNum> skuNums { get; set; }
        public string area { get; set; }
    }

    public class SkuAndNum
    {
        public int skuId { get; set; }
        public int num { get; set; }
    }
}
