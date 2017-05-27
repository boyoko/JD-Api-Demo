using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JD.AutoRunService.SynchronousData.Migrations
{
    public partial class AddProductDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    Appintroduce = table.Column<string>(nullable: true),
                    BrandName = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    EleGift = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    ProductArea = table.Column<string>(nullable: true),
                    SaleUnit = table.Column<string>(nullable: true),
                    Shouhou = table.Column<string>(nullable: true),
                    Sku = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Upc = table.Column<string>(nullable: true),
                    WareQD = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");
        }
    }
}
