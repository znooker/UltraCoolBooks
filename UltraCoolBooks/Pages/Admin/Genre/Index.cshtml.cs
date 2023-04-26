using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Genre
{
    public class IndexModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public IndexModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
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
