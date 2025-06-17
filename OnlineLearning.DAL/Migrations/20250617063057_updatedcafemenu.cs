using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedcafemenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CafeMenuItems_CafeMenuCategories_CategoryId",
                table: "CafeMenuItems");

            migrationBuilder.DropTable(
                name: "CafeMenuCategories");

            migrationBuilder.DropIndex(
                name: "IX_CafeMenuItems_CategoryId",
                table: "CafeMenuItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CafeMenuItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CafeMenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CafeMenuCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeMenuCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CafeMenuItems_CategoryId",
                table: "CafeMenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CafeMenuItems_CafeMenuCategories_CategoryId",
                table: "CafeMenuItems",
                column: "CategoryId",
                principalTable: "CafeMenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
