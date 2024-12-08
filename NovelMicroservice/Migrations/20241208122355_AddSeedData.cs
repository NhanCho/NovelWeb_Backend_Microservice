using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm seed data vào bảng Categories
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Children" },
                    { 2, "Horror" },
                    { 3, "Fantasy" }
                });

            // Thêm seed data vào bảng Novels
            migrationBuilder.InsertData(
                table: "Novels",
                columns: new[] { "NovelID", "Name", "Author", "Description", "CategoryID" },
                values: new object[,]
                {
                    { 1, "The Little Prince", "Antoine de Saint-Exupéry", "A timeless story of a young prince and his adventures.", 1 },
                    { 2, "Dracula", "Bram Stoker", "The classic vampire tale that started it all.", 2 },
                    { 3, "Harry Potter", "J.K. Rowling", "A young wizard's journey through magic and friendship.", 3 }
                });

            // Thêm seed data vào bảng Chapters
            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "ChapterID", "ChapterNumber", "Content", "NovelID" },
                values: new object[,]
                {
                    { 1, 1, "Once upon a time...", 1 },
                    { 2, 2, "The journey begins.", 1 },
                    { 3, 1, "In the shadows of the night.", 2 },
                    { 4, 1, "The boy who lived.", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa dữ liệu seed khi rollback
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValues: new object[] { 1, 2, 3, 4 });
            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValues: new object[] { 1, 2, 3 });
            migrationBuilder.DeleteData(table: "Categories", keyColumn: "CategoryID", keyValues: new object[] { 1, 2, 3 });
        }
    }
}
