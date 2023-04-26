using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Genre
{
    public class CreateModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public CreateModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Genre Genre { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Genres == null || Genre == null)
            {
                
                return Page();
            }

          if (await _context.Genres.AnyAsync(g => g.Title == Genre.Title))
          {
                
                ModelState.AddModelError("Genre.Title", "A genre with that title already exists!");
                return Page();
          }


            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
