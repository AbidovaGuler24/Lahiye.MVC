using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class categoryid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PaidBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaidBooks_CategoryId",
                table: "PaidBooks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidBooks_Categories_CategoryId",
                table: "PaidBooks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidBooks_Categories_CategoryId",
                table: "PaidBooks");

            migrationBuilder.DropIndex(
                name: "IX_PaidBooks_CategoryId",
                table: "PaidBooks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PaidBooks");
        }
    }
}
