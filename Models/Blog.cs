using System.ComponentModel.DataAnnotations;
namespace BlogMVC.Models
{
    public class Blog
    {
        public long BlogID { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        public string? content { get; set; }
        public string? Image { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
