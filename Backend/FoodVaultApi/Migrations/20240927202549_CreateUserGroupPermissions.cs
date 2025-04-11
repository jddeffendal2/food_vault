using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodVaultApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserGroupPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAddRecipes",
                table: "UserGroups",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAddUsers",
                table: "UserGroups",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAddRecipes",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "CanAddUsers",
                table: "UserGroups");
        }
    }
}
