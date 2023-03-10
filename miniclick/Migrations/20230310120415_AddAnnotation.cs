using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniclick.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Urls_Uuid",
                table: "Urls",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Urls_Uuid",
                table: "Urls");
        }
    }
}
