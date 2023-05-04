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

namespace UltraCoolBooks.Pages.Admin.Author
{
    [Authorize(Policy = "AdminPolicy")]
    public class DeleteModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public DeleteModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }
            else 
            {
                Author = author;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            //Get a list of all the books that the author has written
            var books = await _context.Books.Where(b => b.AuthorBooks.Any(ab => ab.AuthorId == id)).Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author).ToListAsync();

            //Get the first author out of the author table (Unknown, Unknown)
            var firstAuthor = await _context.Authors.FirstOrDefaultAsync();


            //Update the found books author to the "Unknown" value in the database, "Unknown" author is the first (authorid = 1) in the database.
            foreach (var item in books)
            {
                //Remove old author connection
                var olddata = _context.AuthorBooks.First(x => x.BooksBookId == item.BookId && x.AuthorId == id);
                _context.AuthorBooks.Remove(olddata);
                _context.SaveChanges();

                //Add new author connection
                AuthorBook newdata = new AuthorBook { BooksBookId = item.BookId, AuthorId = firstAuthor.AuthorId };
                _context.AuthorBooks.Add(newdata);
                _context.SaveChanges();

            }

            //Find the author by id
            var author = await _context.Authors.SingleAsync(x => x.AuthorId == id);

            if (author != null)
            {
                Author = author;
                _context.Authors.Remove(Author);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
