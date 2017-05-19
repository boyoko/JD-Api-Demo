using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JD.AutoRunService.SynchronousData.Migrations
{
    public partial class AddProductSku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSku",
                columns: table => new
                {
                    ProductSkuId = table.Column<string>(nullable: false),
                    PageNum = table.Column<int>(nullable: false),
                    SkuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSku", x => x.ProductSkuId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSku");
        }
    }
}
