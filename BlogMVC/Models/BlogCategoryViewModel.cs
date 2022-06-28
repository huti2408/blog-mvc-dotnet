using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogMVC.Models
{
    public class BlogCategoryViewModel
    {
        public List<Blog>? blogs { get; set; }
        public Blog Blog { get; set; }
        public SelectList? categories { get; set; }
        public string? blogCate { get; set; }
        public string? searchString { get; set; }
    }
}
