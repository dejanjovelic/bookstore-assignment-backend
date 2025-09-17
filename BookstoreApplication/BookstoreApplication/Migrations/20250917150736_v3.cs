using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "Pisac romana i eseja.", new DateTime(1975, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "John Smith" },
                    { 2, "Autorka dečjih knjiga.", new DateTime(1982, 8, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Emily Johnson" },
                    { 3, "Specijalista za istorijske trilere.", new DateTime(1968, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Michael Brown" },
                    { 4, "Piše savremene ljubavne romane.", new DateTime(1990, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Sophia Davis" },
                    { 5, "Poznat po naučno-fantastičnim delima.", new DateTime(1978, 3, 30, 0, 0, 0, 0, DateTimeKind.Utc), "David Wilson" }
                });

            migrationBuilder.InsertData(
                table: "Award",
                columns: new[] { "Id", "AwardStartYear", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Najbolja knjiga godine", "National Book Prize" },
                    { 2, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Najbolje dečje štivo", "Children’s Literature Award" },
                    { 3, new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Najbolji istorijski roman", "Historical Fiction Medal" },
                    { 4, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Najbolja SF knjiga", "Sci-Fi Galaxy Award" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Adress", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Main Street 12", "Sunrise Books", "https://sunrisebooks.com" },
                    { 2, "Harbor Ave 45", "Blue Ocean Press", "https://blueoceanpress.com" },
                    { 3, "Cosmos Blvd 99", "Galaxy Publishing", "https://galaxypublishing.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "Id", "AuthorId", "AwardId", "AwardedOn" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2011, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, 3, new DateTime(2015, 7, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, 2, new DateTime(2016, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, 1, new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 3, 3, new DateTime(2013, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 3, 1, new DateTime(2017, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 3, 4, new DateTime(2019, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 4, 1, new DateTime(2020, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 4, 2, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 4, 3, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 5, 4, new DateTime(2015, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 5, 1, new DateTime(2017, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 5, 3, new DateTime(2018, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 5, 2, new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 1, 4, new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "978000000001", 320, new DateTime(2010, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Rivers of Time" },
                    { 2, 1, "978000000002", 280, new DateTime(2011, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "The Hidden Valley" },
                    { 3, 2, "978000000003", 150, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Magic Adventures" },
                    { 4, 2, "978000000004", 180, new DateTime(2018, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Forest Tales" },
                    { 5, 3, "978000000005", 410, new DateTime(2012, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Empire Shadows" },
                    { 6, 3, "978000000006", 390, new DateTime(2014, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Lost Crown" },
                    { 7, 4, "978000000007", 250, new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Hearts and Stars" },
                    { 8, 4, "978000000008", 230, new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Summer Letters" },
                    { 9, 5, "978000000009", 500, new DateTime(2016, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Cosmic Voyage" },
                    { 10, 5, "978000000010", 450, new DateTime(2018, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Beyond the Stars" },
                    { 11, 5, "978000000011", 470, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Parallel Worlds" },
                    { 12, 1, "978000000012", 430, new DateTime(2022, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Time Horizons" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Award",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Award",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Award",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Award",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
