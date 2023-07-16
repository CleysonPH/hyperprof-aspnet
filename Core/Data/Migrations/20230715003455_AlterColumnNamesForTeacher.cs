using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperProf.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumnNamesForTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "teachers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "teachers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "teachers",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "teachers",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "teachers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "teachers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "teachers",
                newName: "profile_picture");

            migrationBuilder.RenameColumn(
                name: "HourlyPrice",
                table: "teachers",
                newName: "hourly_price");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "teachers",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_teachers_Email",
                table: "teachers",
                newName: "IX_teachers_email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "teachers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "teachers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "teachers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "teachers",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "teachers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "teachers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "teachers",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "hourly_price",
                table: "teachers",
                newName: "HourlyPrice");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "teachers",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_teachers_email",
                table: "teachers",
                newName: "IX_teachers_Email");
        }
    }
}
