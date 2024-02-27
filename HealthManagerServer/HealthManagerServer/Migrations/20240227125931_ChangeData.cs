using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nutritions_Name",
                table: "Nutritions");

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_Name_Serving_size_g",
                table: "Nutritions",
                columns: new[] { "Name", "Serving_size_g" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nutritions_Name_Serving_size_g",
                table: "Nutritions");

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_Name",
                table: "Nutritions",
                column: "Name",
                unique: true);
        }
    }
}
