using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelloApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsComplete", "Name" },
                values: new object[,]
                {
                    { 1L, true, "Make a cup of tea" },
                    { 2L, false, "Tidy your code" },
                    { 3L, false, "Learn something new" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
