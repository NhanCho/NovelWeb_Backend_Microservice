using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Chapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int ChapterID { get; set; }
        public int ChapterNumber { get; set; }
        public string Content { get; set; }

        public int NovelID { get; set; } // Foreign Key
    }
}
