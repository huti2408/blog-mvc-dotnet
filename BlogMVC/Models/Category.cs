using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class Category
    {

        public long Id { get; set; }
        [DisplayName("Category")]
        [Required]
        [StringLength(80)]
        public string? name { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }
    }
}
