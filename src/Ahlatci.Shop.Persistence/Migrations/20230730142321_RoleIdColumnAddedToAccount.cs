using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahlatci.Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoleIdColumnAddedToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ROLE_ID",
                table: "ACCOUNTS",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ROLE_ID",
                table: "ACCOUNTS");
        }
    }
}
