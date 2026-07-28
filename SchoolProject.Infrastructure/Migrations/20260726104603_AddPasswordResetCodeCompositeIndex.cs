using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetCodeCompositeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PasswordResetCodes_HashedCode",
                table: "PasswordResetCodes");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetCodes_UserId_HashedCode",
                table: "PasswordResetCodes",
                columns: new[] { "UserId", "HashedCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PasswordResetCodes_UserId_HashedCode",
                table: "PasswordResetCodes");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetCodes_HashedCode",
                table: "PasswordResetCodes",
                column: "HashedCode");
        }
    }
}
