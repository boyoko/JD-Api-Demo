using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JD.AutoRunService.SynchronousData;

namespace JD.AutoRunService.SynchronousData.Migrations
{
    [DbContext(typeof(JDProductContext))]
    partial class JDProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<long>("SkuId");

                    b.HasKey("ProductSkuId");

                    b.ToTable("ProductSku");
                });
        }
    }
}
