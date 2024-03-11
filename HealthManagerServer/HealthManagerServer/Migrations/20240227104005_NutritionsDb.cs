using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class NutritionsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nutritions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Serving_size_g = table.Column<double>(type: "float", nullable: false),
                    Fat_total_g = table.Column<double>(type: "float", nullable: false),
                    Fat_saturated_g = table.Column<double>(type: "float", nullable: false),
                    Protein_g = table.Column<double>(type: "float", nullable: false),
                    Sodium_mg = table.Column<double>(type: "float", nullable: false),
                    Potassium_mg = table.Column<double>(type: "float", nullable: false),
                    Cholesterol_mg = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates_total_g = table.Column<double>(type: "float", nullable: false),
                    Fiber_g = table.Column<double>(type: "float", nullable: false),
                    Sugar_g = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutritions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_Name",
                table: "Nutritions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nutritions");
        }
    }
}
