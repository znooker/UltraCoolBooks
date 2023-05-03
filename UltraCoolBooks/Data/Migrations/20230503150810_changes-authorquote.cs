using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UltraCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class changesauthorquote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuthorQuote",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuoteText",
                table: "AuthorQuote",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            

            migrationBuilder.CreateIndex(
                name: "IX_AuthorQuote_UserId",
                table: "AuthorQuote",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorQuote_AspNetUsers_UserId",
                table: "AuthorQuote",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorQuote_AspNetUsers_UserId",
                table: "AuthorQuote");

            migrationBuilder.DropIndex(
                name: "IX_AuthorQuote_UserId",
                table: "AuthorQuote");

           

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuthorQuote",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "QuoteText",
                table: "AuthorQuote",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            
        }
    }
}
