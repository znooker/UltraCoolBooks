using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UltraCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedingfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UltraCoolUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewFeedBacks_AspNetUsers_UltraCoolUserId",
                table: "ReviewFeedBacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UltraCoolUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UltraCoolUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_ReviewFeedBacks_UltraCoolUserId",
                table: "ReviewFeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_Books_UltraCoolUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UltraCoolUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UltraCoolUserId",
                table: "ReviewFeedBacks");

            migrationBuilder.DropColumn(
                name: "UltraCoolUserId",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "686d142d-b413-4f82-aba3-8873688c734c", null, "Moderator", "MODERATOR" },
                    { "f8f61d0b-0a2e-4212-9359-4b1fcad1b979", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "016b2ad2-6ee2-4fad-87be-8563b5fd7408", 0, "7a40b78b-3b21-4d24-aef8-41e613f8a044", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAECnN6pQ+R3CSNcbbYPWAl/xI8PzFwRAn5Sej35IGrfWwdEhrSQH0E6GRRZOi1WYUIw==", null, false, "", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "BirthDate", "FirstName", "ImagePath", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1111, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unknown", "no-picture.jpg", "Author" },
                    { 2, new DateTime(1942, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R", "JRR-Tolkien.jpg", "Tolkien" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1942), "William", "William-Gibson.jpg", "Gibson" },
                    { 4, new DateTime(1971, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rafal", "Rafal_Kosik.jpg", "Kosik" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Other genre that haven't been added to our systems yet!", "Other" },
                    { 2, "Fantasy.", "Fantasy" },
                    { 3, "Cyberpunk.", "Cyberpunk" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 1, "Permission", "CanAccessAdminPanel", "f8f61d0b-0a2e-4212-9359-4b1fcad1b979" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f8f61d0b-0a2e-4212-9359-4b1fcad1b979", "016b2ad2-6ee2-4fad-87be-8563b5fd7408" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Description", "ISBN", "ImagePath", "ReleaseDate", "Title", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Set in the dystopian future.", "9780441012039", "Neuromancer.jpg", new DateTime(1984, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neuromancer", "016b2ad2-6ee2-4fad-87be-8563b5fd7408", false },
                    { 2, "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.", "9780261102217", "The_Hobbit.jpg", new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit", "016b2ad2-6ee2-4fad-87be-8563b5fd7408", false },
                    { 3, "Continuing the story begun in The Hobbit, this is the first part of Tolkien's epic masterpiece, The Lord of the Rings, featuring a striking black cover based on Tolkien's own design, the definitive text, and a detailed map of Middle-earth.", "9780261103573", "Fellowship_Of_The_Ring.jpg", new DateTime(1953, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fellowship of the Ring", "016b2ad2-6ee2-4fad-87be-8563b5fd7408", false },
                    { 4, "Ten stories deal with a human memory bank, UFOs, sleep machines, interstellar travel, a Soviet space station, and computer crime", "9780441089345", "Burning_Chrome.jpg", new DateTime(1984, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burning Chrome", "016b2ad2-6ee2-4fad-87be-8563b5fd7408", false },
                    { 5, "Set in the world of Cyberpunk 2077, one of the bestselling video games of recent years, from acclaimed Polish science fiction writer Rafal Kosik, this electrifying novel follows a group of strangers as they discover that the dangers of Night City are all too real.", "0759557179", "No_Coincidence.jpg", new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "No Coincidence", "016b2ad2-6ee2-4fad-87be-8563b5fd7408", false }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorId", "BooksBookId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "BooksBookId", "GenresGenreId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 4, 3 },
                    { 5, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewFeedBacks_AspNetUsers_UserId",
                table: "ReviewFeedBacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewFeedBacks_AspNetUsers_UserId",
                table: "ReviewFeedBacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "686d142d-b413-4f82-aba3-8873688c734c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f8f61d0b-0a2e-4212-9359-4b1fcad1b979", "016b2ad2-6ee2-4fad-87be-8563b5fd7408" });

            migrationBuilder.DeleteData(
                table: "AuthorBook",
                keyColumns: new[] { "AuthorId", "BooksBookId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBook",
                keyColumns: new[] { "AuthorId", "BooksBookId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorBook",
                keyColumns: new[] { "AuthorId", "BooksBookId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBook",
                keyColumns: new[] { "AuthorId", "BooksBookId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorBook",
                keyColumns: new[] { "AuthorId", "BooksBookId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "BooksBookId", "GenresGenreId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f61d0b-0a2e-4212-9359-4b1fcad1b979");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "016b2ad2-6ee2-4fad-87be-8563b5fd7408");

            migrationBuilder.AddColumn<string>(
                name: "UltraCoolUserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UltraCoolUserId",
                table: "ReviewFeedBacks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UltraCoolUserId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UltraCoolUserId",
                table: "Reviews",
                column: "UltraCoolUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewFeedBacks_UltraCoolUserId",
                table: "ReviewFeedBacks",
                column: "UltraCoolUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UltraCoolUserId",
                table: "Books",
                column: "UltraCoolUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UltraCoolUserId",
                table: "Books",
                column: "UltraCoolUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewFeedBacks_AspNetUsers_UltraCoolUserId",
                table: "ReviewFeedBacks",
                column: "UltraCoolUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UltraCoolUserId",
                table: "Reviews",
                column: "UltraCoolUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
