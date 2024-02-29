using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class ActivitiesRepair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaloriesBurnedPerHour = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalCaloriesBurned = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Name",
                table: "Activities",
                column: "Name",
                unique: true);
        }
    }
}
