using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahlatci.Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CustomerAccountIdColumnDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CUSTOMER_ACCOUNT_ACCOUNT_ID",
                table: "CUSTOMERS");

            //migrationBuilder.DropIndex(
            //    name: "IX_CUSTOMERS_ACCOUNT_ID",
            //    table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "ACCOUNT_ID",
                table: "CUSTOMERS");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTS_CUSTOMER_ID",
                table: "ACCOUNTS",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ACCOUNTS_CUSTOMERS_CUSTOMER_ID",
                table: "ACCOUNTS",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACCOUNTS_CUSTOMERS_CUSTOMER_ID",
                table: "ACCOUNTS");

            //migrationBuilder.DropIndex(
            //    name: "IX_ACCOUNTS_CUSTOMER_ID",
            //    table: "ACCOUNTS");

            migrationBuilder.AddColumn<int>(
                name: "ACCOUNT_ID",
                table: "CUSTOMERS",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_ACCOUNT_ID",
                table: "CUSTOMERS",
                column: "ACCOUNT_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "CUSTOMER_ACCOUNT_ACCOUNT_ID",
                table: "CUSTOMERS",
                column: "ACCOUNT_ID",
                principalTable: "ACCOUNTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
