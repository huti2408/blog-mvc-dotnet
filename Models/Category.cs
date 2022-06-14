using System.ComponentModel;

namespace BlogMVC.Models
{
    public class Category
    {

        public long Id { get; set; }
        [DisplayName("Category")]
        public string? name { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }
    }
}
