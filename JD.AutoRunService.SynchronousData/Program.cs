using JD.NetCore.SDK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Z.EntityFramework;
using Z.EntityFramework.Plus;
using System.Data;
using System.Data.SqlClient;
using DapperExtensions.Sql;
using DapperExtensions;
using Dapper;
using System.Collections.Concurrent;
using FastMember;
using Newtonsoft.Json;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;

namespace JD.AutoRunService.SynchronousData
{
    class Program
    {

        private static readonly object obj = new object();
        private static ILog log;
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(repository.Name, "NETCorelog4net");

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

            //using (var context = new JDProductContext())
            //{
            //    if (!context.Database.EnsureCreated())
            //    {
            //        Console.WriteLine("Error: Unable to create the database！");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Create and Connect to database success！");
            //    }
            //}


            SynchronousDataService service = new SynchronousDataService();
            //var token = service.AccessToken().GetAwaiter().GetResult();
            ////同步商品池
            //var x = service.GetPageNum(token).GetAwaiter().GetResult();
            //if (InsertToProductPool(x))
            //{
            //    Console.WriteLine("同步商品池数据成功");
            //}
            //else
            //{
            //    Console.WriteLine("同步商品池数据失败！");
            //}


            //同步每个商品池中的SkuId

            try
            {
                if (InsertToProductSku())
                {
                    Console.WriteLine("同步商品池中的SkuId成功");
                }
                else
                {
                    Console.WriteLine("同步商品池中的SkuId失败！");
                }
            }
            catch(Exception e)
            {
                throw e;
            }


            
            //foreach (var num in GetProductPoolList())
            //{
            //    Console.WriteLine("商品池编号{0},商品池名次{1}",num.PageNum,num.Name);
            //    var skustringArray = service.GetSku(token, num.PageNum.ToString()).GetAwaiter().GetResult();
            //    if (string.IsNullOrWhiteSpace(skustringArray))
            //    {
            //        continue;
            //    }
            //    if (skustringArray.Contains(','))
            //    {
            //        var skuList = skustringArray.Split(',');
            //        Console.WriteLine("商品池编号{0}--商品池名次{1}--有商品Sku{2}个", num.PageNum, num.Name, skuList.Count());
            //        InsertToProductSku(skuList,num);
            //    }
            //    else
            //    {
            //        InsertToProductSku(skustringArray, num);
            //    }


            //}

            //同步商品信息（重点）



            Console.Read();
        }


        private static List<ProductSku> GetSkuList()
        {
            var sw = new Stopwatch();
            sw.Start();
            using (var db = new JDProductContext())
            {

                var x = db.ProductSku.AsNoTracking().Take(10).ToList();
                sw.Stop();
                Console.WriteLine("执行GetSkuList耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                return x;
            }
        }

        private static List<ProductPool> GetProductPoolList()
        {
            var sw = new Stopwatch();
            sw.Start();
            using (var db = new JDProductContext())
            {
                
                var x = db.ProductPool.ToList();
                sw.Stop();
                Console.WriteLine("执行GetProductPoolList耗时：{0}毫秒。",sw.ElapsedMilliseconds);
                return x;
            }
        }


        private static bool InsertToProductPool(List<PageNumResult> list)
        {
            var sw = new Stopwatch();
            sw.Start();
            using (var db = new JDProductContext())
            {
                var tmp = (from c in list
                           select new ProductPool
                           {
                               ProductPoolId = Guid.NewGuid().ToString(),
                               Name = c.name,
                               PageNum = Convert.ToInt32(c.page_num),
                           }).ToList();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("delete  FROM [dbo].[ProductPool]");
                        db.SaveChanges();
                        db.ProductPool.AddRange(tmp);
                        var count = db.SaveChanges();
                        Console.WriteLine("{0} records saved to database", count);
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();
                        sw.Stop();
                        Console.WriteLine("执行InsertToProductPool耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                        sw.Stop();
                        return false;
                    }

                    return true;
                }
            }
        }

        private static bool InsertToProductSku(string[] skus, ProductPool pageNum)
        {
            var sw = new Stopwatch();
            sw.Start();
            var tmp = (from c in skus
                       select new ProductSku
                       {
                           ProductSkuId = Guid.NewGuid().ToString(),
                           ProductPoolId = pageNum.ProductPoolId,
                           PageNum = Convert.ToInt32( pageNum.PageNum),
                           SkuId = Convert.ToInt64(c),
                       }).ToList();
            using (var db = new JDProductContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("delete  FROM [dbo].[ProductSku]");
                        db.SaveChanges();
                        db.ProductSku.AddRange(tmp);
                        var count = db.SaveChanges();
                        Console.WriteLine("{0} records saved to database", count);
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();
                        sw.Stop();
                        Console.WriteLine("执行InsertToProductSku耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                        sw.Stop();
                        return false;
                    }

                    return true;
                }
            }
        }

        private static bool InsertToProductSku(string sku, ProductPool pageNum)
        {
            var sw = new Stopwatch();
            sw.Start();

            var tmp = new ProductSku
            {
                ProductSkuId = Guid.NewGuid().ToString(),
                ProductPoolId = pageNum.ProductPoolId,
                PageNum = Convert.ToInt32(pageNum.PageNum),
                SkuId = Convert.ToInt64(sku),
            };
            using (var db = new JDProductContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("delete  FROM [dbo].[ProductSku]");
                        db.SaveChanges();
                        db.ProductSku.Add(tmp);
                        var count = db.SaveChanges();
                        Console.WriteLine("{0} records saved to database", count);
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();
                        sw.Stop();
                        Console.WriteLine("执行InsertToProductSku-1耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                        sw.Stop();
                        return false;
                    }

                    return true;
                }
                
            }
        }

        private static bool InsertToProductSku_old()
        {
            var sw = new Stopwatch();
            sw.Start();

            SynchronousDataService service = new SynchronousDataService();
            var token = service.AccessToken().GetAwaiter().GetResult();

            List<ProductSku> skus = new List<ProductSku>(5000);

            using (var db = new JDProductContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("delete  FROM [dbo].[ProductSku]");
                        db.SaveChanges();
                        List<ProductPool> list = GetProductPoolList();
                        foreach (var num in list)
                        {
                            Console.WriteLine("商品池编号{0},商品池名次{1}", num.PageNum, num.Name);
                            var skustringArray = service.GetSku(token, num.PageNum.ToString()).GetAwaiter().GetResult();
                            if (string.IsNullOrWhiteSpace(skustringArray))
                            {
                                continue;
                            }
                            if (skustringArray.Contains(','))
                            {
                                var skuList = skustringArray.Split(',');
                                Console.WriteLine("商品池编号{0}--商品池名次{1}--有商品Sku{2}个", num.PageNum, num.Name, skuList.Count());
                                var tmp = (from c in skuList
                                           select new ProductSku
                                           {
                                               ProductSkuId = Guid.NewGuid().ToString(),
                                               ProductPoolId = num.ProductPoolId,
                                               PageNum = Convert.ToInt32(num.PageNum),
                                               SkuId = Convert.ToInt64(c),
                                           }).ToList();
                                skus.AddRange(tmp);

                                //db.ProductSku.AddRange(tmp);
                                //var count = db.SaveChanges();
                                //Console.WriteLine("{0} records saved to database", count);
                            }
                            else
                            {
                                var tmp = new ProductSku
                                {
                                    ProductSkuId = Guid.NewGuid().ToString(),
                                    ProductPoolId = num.ProductPoolId,
                                    PageNum = Convert.ToInt32(num.PageNum),
                                    SkuId = Convert.ToInt64(skustringArray),
                                };
                                skus.Add(tmp);
                                //db.ProductSku.Add(tmp);
                                //var count = db.SaveChanges();
                                //Console.WriteLine("{0} records saved to database", count);
                            }
                        }


                        

                        //list = list.Skip(150).Take(100).ToList();
                        //Parallel.ForEach(list, (num) =>
                        //{

                        //    Console.WriteLine("商品池编号{0},商品池名次{1}", num.PageNum, num.Name);
                        //    var skustringArray = service.GetSku(token, num.PageNum.ToString()).GetAwaiter().GetResult();
                        //    if (string.IsNullOrWhiteSpace(skustringArray))
                        //    {
                        //        Console.WriteLine("#######string.IsNullOrWhiteSpace(skustringArray)###########");
                        //        return;
                        //    }
                        //    if (skustringArray.Contains(','))
                        //    {
                        //        var skuList = skustringArray.Split(',');
                        //        Console.WriteLine("商品池编号{0}--商品池名次{1}--有商品Sku{2}个", num.PageNum, num.Name, skuList.Count());
                        //        var tmp = (from c in skuList
                        //                   select new ProductSku
                        //                   {
                        //                       ProductSkuId = Guid.NewGuid().ToString(),
                        //                       ProductPoolId = num.ProductPoolId,
                        //                       PageNum = Convert.ToInt32(num.PageNum),
                        //                       SkuId = Convert.ToInt64(c),
                        //                   }).ToList();
                        //        skus.AddRange(tmp);
                        //        //lock (obj)
                        //        //{
                        //        //    db.ProductSku.AddRange(tmp);
                        //        //    var count = db.SaveChanges();
                        //        //    Console.WriteLine("{0} records saved to database", count);
                        //        //}

                        //    }
                        //    else
                        //    {
                        //        var tmp = new ProductSku
                        //        {
                        //            ProductSkuId = Guid.NewGuid().ToString(),
                        //            ProductPoolId = num.ProductPoolId,
                        //            PageNum = Convert.ToInt32(num.PageNum),
                        //            SkuId = Convert.ToInt64(skustringArray),
                        //        };
                        //        skus.Add(tmp);
                        //        //lock (obj)
                        //        //{
                        //        //    db.ProductSku.Add(tmp);
                        //        //    var count = db.SaveChanges();
                        //        //    Console.WriteLine("{0} records saved to database", count);
                        //        //}

                        //    }

                        //});

                        //for(var i = 0; i < skus.Count(); i++)
                        //{
                        //    Console.WriteLine("sku:"+skus[i]);
                        //    db.ProductSku.Add(skus[i]);
                        //}

                        db.ProductSku.AddRange(skus);
                        var count = db.SaveChanges();
                        Console.WriteLine("{0} records saved to database", count);

                        sw.Stop();
                        Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);

                        //var conn = db.Database.GetDbConnection();

                        //var tran = conn.BeginTransaction();

                        //InsertBatch(conn, skus, tran).GetAwaiter().GetResult();

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        // TODO: Handle failure
                        sw.Stop();
                        Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                        throw ex;
                        //return false;
                    }

                    return true;
                }

            }

        }



        private static bool InsertToProductSku()
        {
            var sw = new Stopwatch();
            sw.Start();

            SynchronousDataService service = new SynchronousDataService();
            var token = service.AccessToken().GetAwaiter().GetResult();
            using (var db = new JDProductContext())
            {
                try
                {
                    List<ProductPool> list = GetProductPoolList();
                    List<ProductSku> skus = new List<ProductSku>(5000);
                    Parallel.ForEach(list, (num) =>
                    {
                        Console.WriteLine("商品池编号:{0},商品池名称:{1}", num.PageNum, num.Name);
                        var skustringArray = service.GetSku(token, num.PageNum.ToString()).GetAwaiter().GetResult();
                        if (string.IsNullOrWhiteSpace(skustringArray))
                        {
                            Console.WriteLine("#######string.IsNullOrWhiteSpace(skustringArray)###########");
                            return;
                        }
                        if (skustringArray.Contains(','))
                        {
                            var skuList = skustringArray.Split(',');
                            //Console.WriteLine("商品池编号:{0}--商品池名称:{1}--有{2}个Sku", num.PageNum, num.Name, skuList.Count());
                            long x = 0;
                            var tmp = (from c in skuList
                                       select new ProductSku
                                       {
                                           ProductSkuId = Guid.NewGuid().ToString(),
                                           ProductPoolId = num.ProductPoolId,
                                           PageNum = num.PageNum,
                                           SkuId = long.TryParse(c, out x)?x:0
                                       }).ToList();

                            //var jsonStr = JsonConvert.SerializeObject(tmp);
                            //Console.WriteLine("商品池编号:{0}--商品池名称:{1}--有{2}个Sku,序列化结果：{3}", num.PageNum, num.Name, skuList.Count(),jsonStr);
                            //log.Info(jsonStr);
                            skus.AddRange(tmp);
                        }
                        else
                        {
                            var flag = long.TryParse(skustringArray, out long x);
                            var tmp = new ProductSku
                            {
                                ProductSkuId = Guid.NewGuid().ToString(),
                                ProductPoolId = num.ProductPoolId,
                                PageNum = num.PageNum,
                                SkuId = x,
                            };
                            skus.Add(tmp);

                            //var jsonStr = JsonConvert.SerializeObject(tmp);
                            //Console.WriteLine("商品池编号:{0}--商品池名称:{1},序列化结果：{2}", num.PageNum, num.Name, jsonStr);
                            //log.Info(jsonStr);
                        }

                    });

                    sw.Stop();
                    Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);

                    var conn = db.Database.GetDbConnection();
                    InsertBatch(conn, skus).GetAwaiter().GetResult();

                }
                catch (Exception ex)
                {
                    // TODO: Handle failure
                    sw.Stop();
                    Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                    throw ex;
                    //return false;
                }

                return true;

            }

        }


        private static bool InsertToProductSkuBase()
        {
            var sw = new Stopwatch();
            sw.Start();

            SynchronousDataService service = new SynchronousDataService();
            var token = service.AccessToken().GetAwaiter().GetResult();
            using (var db = new JDProductContext())
            {
                try
                {
                    List<ProductPool> list = GetProductPoolList();
                    List<ProductSkuBase> skus = new List<ProductSkuBase>();
                    Parallel.ForEach(list, (num) =>
                    {
                        Console.WriteLine("商品池编号:{0},商品池名称:{1}", num.PageNum, num.Name);
                        var skustringArray = service.GetSku(token, num.PageNum.ToString()).GetAwaiter().GetResult();
                        if (string.IsNullOrWhiteSpace(skustringArray))
                        {
                            Console.WriteLine("#######string.IsNullOrWhiteSpace(skustringArray)###########");
                            return;
                        }
                        Console.WriteLine("商品池编号:{0}--商品池名称:{1}", num.PageNum, num.Name);
                        ProductSkuBase sku = new ProductSkuBase
                        {
                            PageNum = num.PageNum,
                            SkuIds = skustringArray,
                            ProductSkuId = Guid.NewGuid().ToString(),
                            ProductPoolId = num.ProductPoolId
                        };
                        skus.Add(sku);
                    });

                    db.ProductSkuBase.AddRange(skus);
                    var count = db.SaveChanges();
                    sw.Stop();
                    Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    // TODO: Handle failure
                    sw.Stop();
                    Console.WriteLine("执行InsertToProductSku-All耗时：{0}毫秒。", sw.ElapsedMilliseconds);
                    throw ex;
                    //return false;
                }

                return true;

            }

        }



        private static bool Insert()
        {
            try
            {
                List<ProductSku> x = GetSkuList();
                using (var db = new JDProductContext())
                {
                    var conn = db.Database.GetDbConnection();
                    InsertBatch(conn, x).GetAwaiter().GetResult();
                    return true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }



        /// <summary>
        /// 批量插入功能
        /// </summary>
        private static async Task InsertBatch(IDbConnection conn, IEnumerable<ProductSku> entityList, IDbTransaction transaction = null)
        {

            var watch = new System.Diagnostics.Stopwatch();
            
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var tblName = string.Format("dbo.{0}", typeof(ProductSku).Name);

                SqlTransaction tran = null;
                if (transaction == null)
                {
                    tran = (SqlTransaction)conn.BeginTransaction();
                }
                else
                {
                    tran = (SqlTransaction)transaction;
                }
                //using (var bulkCopy = new SqlBulkCopy(conn as SqlConnection, SqlBulkCopyOptions.TableLock, tran))
                using (var bulkCopy = new SqlBulkCopy(conn as SqlConnection, SqlBulkCopyOptions.KeepIdentity, tran))
                {
                    string sqlText = "delete  FROM [dbo].[ProductSku]";
                    SqlCommand cmd = new SqlCommand(sqlText, conn as SqlConnection, tran);
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                    bulkCopy.BatchSize = entityList.Count();
                    //bulkCopy.BatchSize = 5000;
                    bulkCopy.DestinationTableName = tblName;
                    DapperExtensions.Sql.ISqlGenerator sqlGenerator = new SqlGeneratorImpl(new DapperExtensionsConfiguration());
                    var classMap = sqlGenerator.Configuration.GetMap<ProductSku>();
                    var props = classMap.Properties.Where(x => x.Ignored == false).ToArray();
                    foreach (var propertyInfo in props)
                    {
                        bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    }

                    //var copyParameters = new[]
                    //{
                    //    nameof(ProductSku.PageNum),
                    //    nameof(ProductSku.ProductPoolId),
                    //    nameof(ProductSku.ProductSkuId),
                    //    nameof(ProductSku.SkuId),
                    //};

                    watch.Start();
                    bulkCopy.BulkCopyTimeout = 300;


                    //using (var reader = ObjectReader.Create(entityList, copyParameters))
                    //{
                    //    await bulkCopy.WriteToServerAsync(reader);
                    //}

                    //using (var reader = new ObjectDataReader<ProductSku>(entityList))
                    //{
                    //    await bulkCopy.WriteToServerAsync(reader);
                    //}

                    using (var reader = entityList.AsDataReader())
                    {
                        await bulkCopy.WriteToServerAsync(reader);
                    }

                    

                    tran.Commit();
                    watch.Stop();
                    Console.WriteLine("执行WriteToServerAsync耗时：{0}毫秒。", watch.ElapsedMilliseconds);

                }
            }
            catch (AggregateException e)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }




    }
}