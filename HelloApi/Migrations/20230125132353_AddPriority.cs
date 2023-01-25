using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prority",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Prority",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Prority",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Prority",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prority",
                table: "TodoItems");
        }
    }
}
