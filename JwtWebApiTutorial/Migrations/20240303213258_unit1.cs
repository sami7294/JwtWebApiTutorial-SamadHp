using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtWebApiTutorial.Api.Migrations
{
    public partial class unit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetailTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailTb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTb", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserDetailTb",
                columns: new[] { "Id", "FirstName", "LastName", "Name", "Place" },
                values: new object[] { 1, "Samad", "Hasanpour", "Smd", "Tabriz" });

            migrationBuilder.InsertData(
                table: "UserDetailTb",
                columns: new[] { "Id", "FirstName", "LastName", "Name", "Place" },
                values: new object[] { 2, "Sadra", "Hasanpour", "Sdr", "Tabriz" });

            migrationBuilder.InsertData(
                table: "UserDetailTb",
                columns: new[] { "Id", "FirstName", "LastName", "Name", "Place" },
                values: new object[] { 3, "Ali", "Mousavi", "Alm", "Tabriz" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetailTb");

            migrationBuilder.DropTable(
                name: "UserTb");
        }
    }
}
