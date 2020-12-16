using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.API.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            (string password, string salt) = "P@ssw0rd".GenerateSaltAndHashPassword();

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Name", "Email", "Password", "Salt", "RegistrationDate" },
                values: new object[] { Guid.NewGuid(), "Admin", "admin@mail.com", password, salt, DateTime.Now });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
