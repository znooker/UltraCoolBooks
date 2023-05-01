using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UltraCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAddedFlaggedCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "FLaggedCount",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "FLaggedCount",
                table: "Reviews");

            
        }
    }
}
