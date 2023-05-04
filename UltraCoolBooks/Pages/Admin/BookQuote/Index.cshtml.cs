using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.BookQuote
{
    public class IndexModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.BookQuote BookQuote { get; set; } = default!;
        public List<Models.BookQuote> AwaitingReviewQuotes { get; set; }
        public List<Models.BookQuote> AcceptedQuotes { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.AuthorQuote != null)
            {
                AwaitingReviewQuotes = await _context.BookQuote
                .Include(b => b.Book)
                .Where(b => b.IsAccepeted == false)
                .ToListAsync();

                AcceptedQuotes = await _context.BookQuote
                    .Include(b => b.Book)
                    .Where(b => b.IsAccepeted == true)
                    .ToListAsync();
            }

        }

        public async Task<IActionResult> OnPostAcceptQuoteAsync(int? id)
        {
            var quote = await _context.BookQuote.FindAsync(id);
            if (quote != null)
            {
                quote.IsAccepeted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/BookQuote/Index");
        }
        public async Task<IActionResult> OnPostDeleteQuoteAsync(int? id)
        {
            var quote = await _context.BookQuote.FindAsync(id);
            if ( quote != null)
            {
                BookQuote = quote;
                _context.BookQuotes.Remove(quote);
                _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/BookQuote/Index");
        }
    }
}
