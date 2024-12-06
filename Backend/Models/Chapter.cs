namespace Backend.Models
{
    public class Chapter
    {
        public int ChapterID { get; set; }
        public int ChapterNumber { get; set; }
        public string Content { get; set; }

        // Khóa ngoại từ Novel
        public int NovelID { get; set; }
    }
}
