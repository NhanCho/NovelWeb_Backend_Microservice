namespace CommentMicroservice.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int NovelID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
