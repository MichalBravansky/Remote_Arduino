using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Remote_Arduino.Data.Migrations
{
    public partial class Code_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    Code_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sent = table.Column<bool>(nullable: false),
                    Message = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.Code_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Code");
        }
    }
}
