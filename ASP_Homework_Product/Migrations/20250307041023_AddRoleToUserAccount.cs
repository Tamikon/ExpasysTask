using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Homework_Product.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Name",
                keyValue: "admin@gmail.com",
                column: "Role",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
