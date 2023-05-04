using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.AuthorQuotes
{
    public class IndexModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Models.AuthorQuote> AuthorQuotes { get;set; }
        // This is where we will store the TempData from the create model
        public string SubmitConfirmation { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.AuthorQuote != null)
            {
                AuthorQuotes = await _context.AuthorQuote
                .Include(a => a.Author)
                .Where(a => a.IsAccepeted == true)
                .ToListAsync();
                // Takes data from AuthorQuotes/Create and puts it into a property that can be used in the index page
                SubmitConfirmation = TempData["SubmitConfirmation"] as string;
            }
            
        }
    }
}
