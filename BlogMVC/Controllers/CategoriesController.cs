using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BlogContext _context;

        public CategoriesController(BlogContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            if (IsAdmin())
            {
                return _context.Categories != null ?
                         View(await _context.Categories.ToListAsync()) :
                         Problem("Entity set 'BlogContext.Categories'  is null.");

            }
            return RedirectToAction("Home", "Blogs");

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            if (IsAdmin())
            {
                var category = await _context.Categories
                 .FirstOrDefaultAsync(m => m.Id == id);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);

            }
            return RedirectToAction("Home", "Blogs");

        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            if (IsAdmin())
            {

                return View();

            }
            return RedirectToAction("Home", "Blogs");
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            if (IsAdmin())
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);

            }
            return RedirectToAction("Home", "Blogs");

        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            if (IsAdmin())
            {
                var category = await _context.Categories
                                .FirstOrDefaultAsync(m => m.Id == id);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);

            }
            return RedirectToAction("Home", "Blogs");

        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'BlogContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public bool IsAdmin()
        {
            if (HttpContext.Session.GetInt32("_ROLE") != 0)
            {
                return false;
            }
            return true;
        }
        private bool CategoryExists(long id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
