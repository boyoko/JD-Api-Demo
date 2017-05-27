using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JD.AutoRunService.SynchronousData;

namespace JD.AutoRunService.SynchronousData.Migrations
{
    [DbContext(typeof(JDProductContext))]
    [Migration("20170527025116_AddProductSkuBase")]
    partial class AddProductSkuBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JD.AutoRunService.SynchronousData.ProductDetail", b =>
                {
                    b.Property<string>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appintroduce");

                    b.Property<string>("BrandName");

                    b.Property<string>("Category");

                    b.Property<int>("EleGift");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Introduction");

                    b.Property<string>("Name");

                    b.Property<string>("Param");

                    b.Property<string>("ProductArea");

                    b.Property<string>("SaleUnit");

                    b.Property<string>("Shouhou");

                    b.Property<int>("Sku");

                    b.Property<int>("State");

                    b.Property<string>("Upc");

                    b.Property<string>("WareQD");

                    b.Property<string>("Weight");

                    b.HasKey("ProductId");

                    b.ToTable("ProductDetail");
                });

            modelBuilder.Entity("JD.AutoRunService.SynchronousData.ProductPool", b =>
                {
                    b.Property<string>("ProductPoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PageNum");

                    b.HasKey("ProductPoolId");

                    b.ToTable("ProductPool");
                });

            modelBuilder.Entity("JD.AutoRunService.SynchronousData.ProductSku", b =>
                {
                    b.Property<string>("ProductSkuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PageNum");

                    b.Property<string>("ProductPoolId");

                    b.Property<long>("SkuId");

                    b.HasKey("ProductSkuId");

                    b.ToTable("ProductSku");
                });

            modelBuilder.Entity("JD.AutoRunService.SynchronousData.ProductSkuBase", b =>
                {
                    b.Property<string>("ProductSkuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PageNum");

                    b.Property<string>("ProductPoolId");

                    b.Property<string>("SkuIds");

                    b.HasKey("ProductSkuId");

                    b.ToTable("ProductSkuBase");
                });
        }
    }
}
