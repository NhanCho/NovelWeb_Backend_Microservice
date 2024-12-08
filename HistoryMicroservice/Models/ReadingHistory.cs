namespace HistoryMicroservice.Models
{
    public class ReadingHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NovelId { get; set; }
        public int? ChapterID { get; set; }
        public DateTime LastReadDate { get; set; }
    }

}
