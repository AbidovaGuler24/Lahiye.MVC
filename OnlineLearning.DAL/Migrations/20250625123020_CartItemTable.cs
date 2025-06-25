using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CartItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PaidBooks_BookId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "CartItems",
                newName: "PaidBookId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                newName: "IX_CartItems_PaidBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PaidBooks_PaidBookId",
                table: "CartItems",
                column: "PaidBookId",
                principalTable: "PaidBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PaidBooks_PaidBookId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "PaidBookId",
                table: "CartItems",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_PaidBookId",
                table: "CartItems",
                newName: "IX_CartItems_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PaidBooks_BookId",
                table: "CartItems",
                column: "BookId",
                principalTable: "PaidBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
