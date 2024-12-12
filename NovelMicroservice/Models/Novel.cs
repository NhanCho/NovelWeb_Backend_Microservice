using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovelMicroservice.Models
{
    public class Novel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int NovelID { get; set; }
        public string ImageUrl { get; set; } // Thêm trường này để lưu URL hình ảnh

        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; } // Foreign Key

    }
}
