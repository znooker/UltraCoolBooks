using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UltraCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedingBetter : Migration
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
                values: new object[] { "b15daefd-57ab-425a-bf86-4d1f7e24f946", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6a7d1edb-9373-4505-be5d-fba6f2560ee1", 0, "d4273c44-bac4-4cc9-b6dd-6d9029038865", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAjtVD6+z0vtdo9Ia9/0ERz3Z57sbMdD3yElAVkc48nIMljCqr6sghG+c3HFLOaJqw==", null, false, "", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "BirthDate", "FirstName", "ImagePath", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1111, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unknown", "", "Author" },
                    { 2, new DateTime(1942, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R", "https://cdn.britannica.com/65/66765-050-63A945A7/JRR-Tolkien.jpg", "Tolkien" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1942), "William", "", "Gibson" },
                    { 4, new DateTime(1971, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rafal", "", "Kosik" }
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
                values: new object[] { 1, "Permission", "CanAccessAdminPanel", "b15daefd-57ab-425a-bf86-4d1f7e24f946" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b15daefd-57ab-425a-bf86-4d1f7e24f946", "6a7d1edb-9373-4505-be5d-fba6f2560ee1" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Description", "ISBN", "ImagePath", "ReleaseDate", "Title", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Set in the dystopian future.", "9780441012039", "https://external-preview.redd.it/k-PCe5oPyJQxPzROwuOgRFPi7MPewZ10KwkGisBDJtE.jpg?width=640&crop=smart&auto=webp&s=5c0a19fd2d1adcafb282dbbd411e31c1d97bc103", new DateTime(1984, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neuromancer", "6a7d1edb-9373-4505-be5d-fba6f2560ee1", false },
                    { 2, "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.", "9780261102217", "https://s1.adlibris.com/images/206575/the-hobbit.jpg", new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit", "6a7d1edb-9373-4505-be5d-fba6f2560ee1", false },
                    { 3, "Continuing the story begun in The Hobbit, this is the first part of Tolkien's epic masterpiece, The Lord of the Rings, featuring a striking black cover based on Tolkien's own design, the definitive text, and a detailed map of Middle-earth.", "9780261103573", "https://s2.adlibris.com/images/6053898/fellowship-of-the-ring.jpg", new DateTime(1953, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fellowship of the Ring", "6a7d1edb-9373-4505-be5d-fba6f2560ee1", false },
                    { 4, "Ten stories deal with a human memory bank, UFOs, sleep machines, interstellar travel, a Soviet space station, and computer crime", "9780441089345", "https://pictures.abebooks.com/isbn/9780441089345-us.jpg", new DateTime(1984, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burning Chrome", "6a7d1edb-9373-4505-be5d-fba6f2560ee1", false },
                    { 5, "Set in the world of Cyberpunk 2077, one of the bestselling video games of recent years, from acclaimed Polish science fiction writer Rafal Kosik, this electrifying novel follows a group of strangers as they discover that the dangers of Night City are all too real.", "0759557179", "https://m.media-amazon.com/images/I/51F+aTeO11L._SX327_BO1,204,203,200_.jpg", new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "No Coincidence", "6a7d1edb-9373-4505-be5d-fba6f2560ee1", false }
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
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b15daefd-57ab-425a-bf86-4d1f7e24f946", "6a7d1edb-9373-4505-be5d-fba6f2560ee1" });

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
                keyValue: "b15daefd-57ab-425a-bf86-4d1f7e24f946");

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
                keyValue: "6a7d1edb-9373-4505-be5d-fba6f2560ee1");

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
