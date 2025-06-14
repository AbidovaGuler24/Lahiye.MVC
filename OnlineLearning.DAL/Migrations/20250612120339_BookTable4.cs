using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BookTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "books",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "books",
                newName: "PdfUrl");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "books",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_AuthorId",
                table: "books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_books_CategoryId",
                table: "books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Author_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Category_CategoryId",
                table: "books",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Author_AuthorId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Category_CategoryId",
                table: "books");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_books_AuthorId",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_CategoryId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "PdfUrl",
                table: "books",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "books",
                newName: "Year");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
