using JD.NetCore.SDK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JD.AutoRunService.SynchronousData
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //FluentScheduler.JobManager.AddJob(() =>
            //{
            //    SynchronousDataService service = new SynchronousDataService();
            //    var token = service.AccessToken().GetAwaiter().GetResult();
            //    GetCategorysRequestDto request = new GetCategorysRequestDto
            //    {
            //        token = token,
            //        pageNo = 2,
            //        pageSize = 5,

            //    };

            //    var x = service.GetCategorys(request).GetAwaiter().GetResult();
            //    //var x = service.GetCategorys(token, "3670993,3670994").GetAwaiter().GetResult();
            //}, t =>
            //{
            //    t.ToRunNow().AndEvery(24).Hours();
            //});

            SynchronousDataService service = new SynchronousDataService();
            var token = service.AccessToken().GetAwaiter().GetResult();
            var x = service.GetPageNum(token).GetAwaiter().GetResult();
            if (InsertToProductPool(x))
            {
                Console.WriteLine("同步数据成功");
            }
            else
            {
                Console.WriteLine("同步数据失败！");
            }


            //同步每个商品池中的SkuId
            foreach(var num in x)
            {
                Console.WriteLine("商品池编号{0},商品池名次{1}",num.page_num,num.name);
                var skuList = service.GetSku(token, num.page_num).GetAwaiter().GetResult().Split(',');
                Console.WriteLine("商品池编号{0}--商品池名次{1}--有商品Sku{2}个", num.page_num, num.name,skuList.Count());

            }




            Console.Read();
        }




        private static bool InsertToProductPool(List<PageNumResult> list)
        {
            using (var db = new JDProductContext())
            {

                var tmp = (from c in list
                           select new ProductPool
                           {
                               ProductPoolId = Guid.NewGuid().ToString(),
                               Name = c.name,
                               PageNum =Convert.ToInt32(c.page_num),
                           }).ToList();

                db.ProductPool.AddRange(tmp);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);
                return true;
            }
        }


        private static bool InsertToProductSku(string[] skus, PageNumResult pageNum)
        {
            var tmp = (from c in skus
                       select new ProductSku
                       {
                           ProductSkuId = Guid.NewGuid().ToString(),
                           PageNum = Convert.ToInt32( pageNum.page_num),
                           SkuId = Convert.ToInt64(c),
                       }).ToList();
            using (var db = new JDProductContext())
            {
                db.ProductSku.AddRange(tmp);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);
            }
            return true;
        }




    }
}