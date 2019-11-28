using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cell_phone",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "First_name",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Home_phone",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IRD",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Middle_initial",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Office_ext",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "active",
                table: "Contacts",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cell_phone",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "First_name",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Home_phone",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IRD",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Middle_initial",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Office_ext",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "active",
                table: "Contacts");
        }
    }
}
