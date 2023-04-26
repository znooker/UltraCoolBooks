using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Book
{
    public class DetailsModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public DetailsModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Models.Book Book { get; set; } = default!; 
      public List<Models.Author> Authors { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            //All Genres connected to the book.
            var book = await _context.Books.Include(bg => bg.BookGenres).ThenInclude(g => g.GenresGenre).FirstOrDefaultAsync(m => m.BookId == id);
            
            //All authors connected to the book
            var authors = _context.Authors.
               Include(author => author.AuthorBooks).
               ThenInclude(ab => ab.BooksBook).
               Where(author => author.AuthorBooks.
               Any(ab => ab.BooksBookId == id)).ToList();

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Authors = authors;
                Book = book;
            }
            return Page();
        }
    }
}
