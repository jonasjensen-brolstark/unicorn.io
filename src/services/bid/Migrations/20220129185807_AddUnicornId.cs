using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bid.Migrations
{
    public partial class AddUnicornId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnicornId",
                table: "Bids",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnicornId",
                table: "Bids");
        }
    }
}
