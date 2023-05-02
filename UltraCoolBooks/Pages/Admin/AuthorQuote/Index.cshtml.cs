using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.AuthorQuote
{
    public class IndexModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.AuthorQuote AuthorQuote { get; set; } = default!;
        public List<Models.AuthorQuote> AwaitingReviewQuotes { get; set; }
        public List<Models.AuthorQuote> AcceptedQuotes { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.AuthorQuote != null)
            {
                AwaitingReviewQuotes = await _context.AuthorQuote
                .Include(a => a.Author)
                .Where(a => a.IsAccepeted == false)
                .ToListAsync();

                AcceptedQuotes = await _context.AuthorQuote
                    .Include(a => a.Author)
                    .Where(a => a.IsAccepeted == true)
                    .ToListAsync();
            }

        }

        public async Task<IActionResult> OnPostAcceptQuoteAsync(int? id)
        {
            var quote = await _context.AuthorQuote.FindAsync(id);
            if (quote != null)
            {
                quote.IsAccepeted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/AuthorQuote/Index");
        }
        public async Task<IActionResult> OnPostDeleteQuoteAsync(int? id)
        {
            var quote = await _context.AuthorQuote.FindAsync(id);
            if ( quote != null)
            {
                AuthorQuote = quote;
                _context.AuthorQuotes.Remove(quote);
                _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/AuthorQuote/Index");
        }
    }
}
