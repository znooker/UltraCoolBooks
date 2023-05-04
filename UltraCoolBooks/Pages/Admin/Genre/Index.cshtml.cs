using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Genre
{
    [Authorize(Policy = "AdminPolicy")]
    public class IndexModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Genre> Genre { get;set; } = default!;

        public async Task OnGetAsync()
        {


            if (_context.Genres != null)
            {
                Genre = await _context.Genres.Include(bg => bg.BookGenres).ThenInclude(b => b.BooksBook).ToListAsync();
            }
        }
    }
}
