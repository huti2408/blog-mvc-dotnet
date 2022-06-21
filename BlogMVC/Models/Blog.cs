using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMVC.Models
{
    public class Blog
    {
        public long BlogID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime? CreatedDate { get; set; }
        [Required]
        public string? content { get; set; }
        [DisplayName("Image Name:")]
        public string? Image { get; set; }
        [NotMapped]
        [DisplayName("Upload File:")]
        public IFormFile? ImageFile { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; } 
        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
