using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JD.AutoRunService.SynchronousData
{
    public class JDProductContext:DbContext
    {

        public virtual DbSet<ProductPool> ProductPool { get; set; }
        public virtual DbSet<ProductSku> ProductSku { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }
    }

    public class ProductPool
    {
        [Key]
        public string ProductPoolId { get; set; }
        public string Name { get; set; }
        public int PageNum { get; set; }
    }

    public class ProductSku
    {
        [Key]
        public string ProductSkuId { get; set; }
        //public string ProductPoolId { get; set; }
        public int PageNum { get; set; }
        public long SkuId { get; set; }
    }



}
