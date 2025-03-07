using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Homework_Product.Migrations
{
    public partial class UpdateUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем IsBlocked
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            // Преобразуем данные в Role перед изменением типа
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Role\" TYPE integer USING CASE " +
                                 "WHEN \"Role\" = 'Admin' THEN 2 " +
                                 "WHEN \"Role\" = 'User' THEN 1 " +
                                 "ELSE 0 END;");

            // Указываем, что Role теперь integer и NOT NULL
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстанавливаем IsBlocked
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Восстанавливаем Role как text
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Role\" TYPE text USING CASE " +
                                 "WHEN \"Role\" = 0 THEN NULL " +
                                 "WHEN \"Role\" = 1 THEN 'User' " +
                                 "WHEN \"Role\" = 2 THEN 'Admin' END;");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}