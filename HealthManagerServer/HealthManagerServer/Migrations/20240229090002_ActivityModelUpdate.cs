using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class ActivityModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCaloriesBurned",
                table: "Activities",
                newName: "Total_calories");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Activities",
                newName: "Duration_minutes");

            migrationBuilder.RenameColumn(
                name: "CaloriesBurnedPerHour",
                table: "Activities",
                newName: "Calories_per_hour");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_Name_Duration_Weight",
                table: "Activities",
                newName: "IX_Activities_Name_Duration_minutes_Weight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total_calories",
                table: "Activities",
                newName: "TotalCaloriesBurned");

            migrationBuilder.RenameColumn(
                name: "Duration_minutes",
                table: "Activities",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Calories_per_hour",
                table: "Activities",
                newName: "CaloriesBurnedPerHour");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_Name_Duration_minutes_Weight",
                table: "Activities",
                newName: "IX_Activities_Name_Duration_Weight");
        }
    }
}
