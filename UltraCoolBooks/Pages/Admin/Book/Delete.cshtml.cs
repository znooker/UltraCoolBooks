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

namespace UltraCoolBooks.Pages.Admin.Book
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
      public Models.Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                Book = book;
                Book.isDeleted = true;
                //_context.Books.Add(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
