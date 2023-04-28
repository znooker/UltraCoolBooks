using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Book
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty, Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public IList<Models.Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books.Include(b => b.User).ToListAsync();
            }
        }
    }
}
