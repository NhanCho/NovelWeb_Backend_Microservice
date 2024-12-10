using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; } // Thêm trường này để lưu URL hình ảnh

    }
}

