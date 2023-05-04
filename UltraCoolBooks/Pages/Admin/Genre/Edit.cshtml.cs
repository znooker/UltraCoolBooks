using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Genre
{
    [Authorize(Policy = "AdminPolicy")]
    public class EditModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public EditModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre =  await _context.Genres.FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }
            Genre = genre;
            return Page();
        }
      
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // check if a genre with the same id already exists in the database
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == Genre.GenreId);

            //check if the genre beeing edited has the same id as the one found in the database
            if (existingGenre != null && Genre.GenreId == existingGenre.GenreId)
            {
                //check if conflicting titlename already exists in database
                var existingGenreTitle = await _context.Genres.FirstOrDefaultAsync(g => g.Title == Genre.Title);
                if (existingGenreTitle != null  && Genre.Title  == existingGenreTitle.Title && Genre.GenreId != existingGenreTitle.GenreId){
                    ModelState.AddModelError("Genre.Title", "A genre with that title already exists!");
                    return Page();
                }


                //update the existing genre record
                existingGenre.Title = Genre.Title;
                existingGenre.Description = Genre.Description;

                // save changes to the database
                await _context.SaveChangesAsync();

                // redirect to the genre list page
                return RedirectToPage("./Index");
            }

           
            ModelState.AddModelError("Genre.Title", "A genre with that title already exists!");
            return Page();
            

        }



    }
}
