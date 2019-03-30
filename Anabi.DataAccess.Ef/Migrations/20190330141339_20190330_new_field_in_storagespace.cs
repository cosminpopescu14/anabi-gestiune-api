using Microsoft.EntityFrameworkCore.Migrations;

namespace Anabi.DataAccess.Ef.Migrations
{
    public partial class _20190330_new_field_in_storagespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageSpacesType",
                table: "StorageSpaces",
                nullable: false,
                defaultValue: 0);

            /*migrationBuilder.AlterColumn<string>(
                name: "UserCodeLastChange",
                table: "Addresses",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCodeAdd",
                table: "Addresses",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stair",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FlatNo",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Building",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageSpacesType",
                table: "StorageSpaces");

            migrationBuilder.AlterColumn<string>(
                name: "UserCodeLastChange",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCodeAdd",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Stair",
                table: "Addresses",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FlatNo",
                table: "Addresses",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Building",
                table: "Addresses",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }*/
        }
    }
}
