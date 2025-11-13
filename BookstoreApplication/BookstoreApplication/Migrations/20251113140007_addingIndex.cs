using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class addingIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Books",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "AverageRating",
                value: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FullName",
                table: "Authors",
                column: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_FullName",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "AverageRating",
                table: "Books",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "AverageRating",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "AverageRating",
                value: 0);
        }
    }
}
