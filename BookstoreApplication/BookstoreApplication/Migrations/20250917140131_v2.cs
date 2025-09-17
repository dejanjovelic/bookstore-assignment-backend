using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardRecord_Authors_AuthorId",
                table: "AuthorAwardRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardRecord_Award_AwardId",
                table: "AuthorAwardRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorAwardRecord",
                table: "AuthorAwardRecord");

            migrationBuilder.RenameTable(
                name: "AuthorAwardRecord",
                newName: "AuthorAwardBridge");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Authors",
                newName: "Birthday");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardRecord_AwardId",
                table: "AuthorAwardBridge",
                newName: "IX_AuthorAwardBridge_AwardId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardRecord_AuthorId",
                table: "AuthorAwardBridge",
                newName: "IX_AuthorAwardBridge_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Award_AwardId",
                table: "AuthorAwardBridge",
                column: "AwardId",
                principalTable: "Award",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Award_AwardId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge");

            migrationBuilder.RenameTable(
                name: "AuthorAwardBridge",
                newName: "AuthorAwardRecord");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Authors",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardBridge_AwardId",
                table: "AuthorAwardRecord",
                newName: "IX_AuthorAwardRecord_AwardId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardBridge_AuthorId",
                table: "AuthorAwardRecord",
                newName: "IX_AuthorAwardRecord_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorAwardRecord",
                table: "AuthorAwardRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardRecord_Authors_AuthorId",
                table: "AuthorAwardRecord",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardRecord_Award_AwardId",
                table: "AuthorAwardRecord",
                column: "AwardId",
                principalTable: "Award",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
