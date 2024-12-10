using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelMicroservice.Migrations
{
    public partial class AddImageUrlToSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing data
            migrationBuilder.DeleteData(table: "Categories", keyColumn: "CategoryID", keyValue: 4);
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 5);
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 6);
            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValue: 5);
            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValue: 6);

            // Add new columns for ImageUrl
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Novels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            // Seed Category Data
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Children", "https://media.istockphoto.com/id/177321629/vi/anh/nh%C3%B3m-tr%E1%BA%BB-em-c%C3%B3-d%E1%BA%A5u-hi%E1%BB%87u-gi%C6%A1-ng%C3%B3n-tay-c%C3%A1i-l%C3%AAn.jpg?s=2048x2048&w=is&k=20&c=osGSOiFdJ-A3QB5UOD06R22vFACOAc9A5MYddfV4EBs=" },
                    { 2, "Horror", "https://media.istockphoto.com/id/1198829958/photo/group-of-five-scary-figures-in-hooded-cloaks-in-the-dark.jpg?s=612x612&w=0&k=20&c=Vjx6Kz6zpdqPrUr1RAUyXWwcOlsy64vd6_ENdPl-r0E=" },
                    { 3, "Fantasy", "https://media.istockphoto.com/id/688410346/vector/chinese-style-fantasy-scenes.jpg?s=612x612&w=0&k=20&c=r3skS5InspYQ7EqUCCzUzU3QHcwRwD6mNRbDpP8sIG4=" }
                });

            // Seed Novel Data
            migrationBuilder.InsertData(
                table: "Novels",
                columns: new[] { "NovelID", "Name", "Author", "Description", "CategoryID", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "The Little Prince", "Antoine de Saint-Exupéry", "A timeless story of a young prince and his adventures.", 1, "https://images-na.ssl-images-amazon.com/images/I/71OZY035QKL.jpg" },
                    { 2, "Dracula", "Bram Stoker", "The classic vampire tale that started it all.", 2, "https://cdn.kobo.com/book-images/88a05cf1-a3b6-461b-a8f7-f0e25b06274a/1200/1200/False/dracula-bram-stoker.jpg" },
                    { 3, "Harry Potter", "J.K. Rowling", "A young wizard's journey through magic and friendship.", 3, "https://nhasachphuongnam.com/images/detailed/160/81YOuOGFCJL.jpg" }
                });

            // Seed Chapter Data
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete seeded data
            migrationBuilder.DeleteData(table: "Categories", keyColumn: "CategoryID", keyValue: 1);
            migrationBuilder.DeleteData(table: "Categories", keyColumn: "CategoryID", keyValue: 2);
            migrationBuilder.DeleteData(table: "Categories", keyColumn: "CategoryID", keyValue: 3);

            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 1);
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 2);
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 3);
            migrationBuilder.DeleteData(table: "Chapters", keyColumn: "ChapterID", keyValue: 4);

            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValue: 1);
            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValue: 2);
            migrationBuilder.DeleteData(table: "Novels", keyColumn: "NovelID", keyValue: 3);

            // Drop the ImageUrl columns
            migrationBuilder.DropColumn(name: "ImageUrl", table: "Novels");
            migrationBuilder.DropColumn(name: "ImageUrl", table: "Categories");

            // Restore previous seed data
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[] { 4, "Horror" });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "ChapterID", "ChapterNumber", "Content", "NovelID" },
                values: new object[,]
                {
                    { 5, 1, "Once upon a time...", 5 },
                    { 6, 1, "It was a dark and stormy night...", 6 }
                });

            migrationBuilder.InsertData(
                table: "Novels",
                columns: new[] { "NovelID", "Author", "CategoryID", "Description", "Name" },
                values: new object[,]
                {
                    { 5, "Author A", 3, "A fun tale for kids", "Little Adventures" },
                    { 6, "Author B", 4, "A scary story", "Haunted House" }
                });
        }
    }
}
