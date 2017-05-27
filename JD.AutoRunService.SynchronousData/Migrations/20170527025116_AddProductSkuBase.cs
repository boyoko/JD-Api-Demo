using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JD.AutoRunService.SynchronousData.Migrations
{
    public partial class AddProductSkuBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSkuBase",
                columns: table => new
                {
                    ProductSkuId = table.Column<string>(nullable: false),
                    PageNum = table.Column<int>(nullable: false),
                    ProductPoolId = table.Column<string>(nullable: true),
                    SkuIds = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSkuBase", x => x.ProductSkuId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSkuBase");
        }
    }
}
