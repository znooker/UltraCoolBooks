﻿using System;
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

        public async Task OnGetAsync()
        {
            if (_context.AuthorQuote != null)
            {
                AuthorQuotes = await _context.AuthorQuote
                .Include(a => a.Author)
                .Where(a => a.IsAccepeted == true)
                .ToListAsync();
            }
            
        }
    }
}
