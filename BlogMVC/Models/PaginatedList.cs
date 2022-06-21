namespace BlogMVC.Models
{
    public class PaginatedList<T>
    {
        public BlogCategoryViewModel BlogCateVM { get; set; }
        public string? blogCate { get; set; }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(BlogCategoryViewModel blogCateVM, int count, int pageIndex, int pageSize)
        {
            BlogCateVM = blogCateVM;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public static PaginatedList<T> CreateAsync(BlogCategoryViewModel BlogCateVM, int pageIndex, int pageSize)
        {
            var source = BlogCateVM.blogs;
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            BlogCateVM.blogs = items;
            return new PaginatedList<T>(BlogCateVM, count, pageIndex, pageSize);
        }
    }
}
