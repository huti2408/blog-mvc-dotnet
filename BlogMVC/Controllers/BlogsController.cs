using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BlogsController(BlogContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Blogs
        public async Task<IActionResult> Index(string searchString, string blogCate, int? pageNumber)
        {
            IQueryable<string> cateQuery = from b in _context.Blogs orderby b.CategoryId select b.Category.name;
            var blogs = from b in _context.Blogs select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title!.Contains(searchString));
                pageNumber = 1;
            }

            if (!string.IsNullOrEmpty(blogCate))
            {
                blogs = blogs.Where(b => b.Category.name == blogCate);

            }
            int pageSize = 3;
            var BlogCateVM = new BlogCategoryViewModel
            {
                categories = new SelectList(await cateQuery.Distinct().ToListAsync()),
                blogs = await blogs.ToListAsync(),
            };
            var PagedList = PaginatedList<BlogCategoryViewModel>.CreateAsync(BlogCateVM, pageNumber ?? 1, pageSize);
            return View(PagedList);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BlogID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "name");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogID,Title,CreatedDate,content,ImageFile,CategoryId")] Blog blog)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(blog.ImageFile.FileName);
            string extension = Path.GetExtension(blog.ImageFile.FileName);
            blog.Image = fileName = fileName+ DateTime.Now.ToString("yymmss") + extension;
            string path = Path.Combine(wwwRootPath+"/Image", fileName);
            using(var fileStream = new FileStream(path, FileMode.Create))
            {
                await blog.ImageFile.CopyToAsync(fileStream);
            }
            _context.Add(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "name", blog.CategoryId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("BlogID,Title,CreatedDate,content,Image,CategoryId")] Blog blog)
        {
            if (id != blog.BlogID)
            {
                return NotFound();
            }


            try
            {
                _context.Entry(blog).State = EntityState.Modified;
                _context.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(blog.BlogID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BlogID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'BlogContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(long id)
        {
            return (_context.Blogs?.Any(e => e.BlogID == id)).GetValueOrDefault();
        }
    }
}
