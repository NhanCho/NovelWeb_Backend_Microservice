namespace Backend.Models
{
    public class CommentDto
    {
        public int CommentID { get; set; }
        public int NovelID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
