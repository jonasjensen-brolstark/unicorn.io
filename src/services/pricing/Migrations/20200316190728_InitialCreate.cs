using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pricing.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Pricings",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            UnicornId = table.Column<Guid>(nullable: false),
            Price = table.Column<double>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Pricings", x => x.Id);
          });

      migrationBuilder.InsertData(
        table: "Pricings",
        columns: new[] { "Id", "UnicornId", "Price" },
        values: new object[] { Guid.NewGuid(), Guid.Parse("2bfb422d-7b50-41e7-86df-ecfc1ebde843"), 120.50 }
      );
      migrationBuilder.InsertData(
        table: "Pricings",
        columns: new[] { "Id", "UnicornId", "Price" },
        values: new object[] { Guid.NewGuid(), Guid.Parse("0fd000a6-2cbf-4545-b7dd-099ebb170abf"), 95.99 }
      );
      migrationBuilder.InsertData(
        table: "Pricings",
        columns: new[] { "Id", "UnicornId", "Price" },
        values: new object[] { Guid.NewGuid(), Guid.Parse("586adccb-d597-4eb0-b992-f43280c8ff5d"), 42.75 }
      );
      migrationBuilder.InsertData(
        table: "Pricings",
        columns: new[] { "Id", "UnicornId", "Price" },
        values: new object[] { Guid.NewGuid(), Guid.Parse("ed4ef265-d893-4fec-8ba9-743906c066e1"), 10.00 }
      );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Pricings");
    }
  }
}
