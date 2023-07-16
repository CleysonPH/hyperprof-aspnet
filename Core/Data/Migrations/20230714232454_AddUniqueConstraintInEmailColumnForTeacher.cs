using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperProf.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintInEmailColumnForTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_teachers_Email",
                table: "teachers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_teachers_Email",
                table: "teachers");
        }
    }
}
