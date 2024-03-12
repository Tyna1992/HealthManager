using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Calories_per_hour = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Duration_minutes = table.Column<int>(type: "int", nullable: false),
                    Total_calories = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.Id);
                });

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
                name: "IX_Activities_Name_Duration_minutes_Weight",
                table: "Activities",
                columns: new[] { "Name", "Duration_minutes", "Weight" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_Name",
                table: "Cocktails",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_Name_Serving_size_g",
                table: "Nutritions",
                columns: new[] { "Name", "Serving_size_g" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "Nutritions");
        }
    }
}
