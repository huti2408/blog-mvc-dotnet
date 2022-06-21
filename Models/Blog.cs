using System.ComponentModel.DataAnnotations;

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
        public string? Image { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
