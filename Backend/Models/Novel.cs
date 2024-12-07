namespace Backend.Models
{
    public class Novel
    {
        public int NovelID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
    }
}
