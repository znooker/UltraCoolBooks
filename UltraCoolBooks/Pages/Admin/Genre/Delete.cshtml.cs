using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Genre
{
    public class DeleteModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public DeleteModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Genre Genre { get; set; } = default!;
        [BindProperty]
        public Models.BookGenre BookGenre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(m => m.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                Genre = genre;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            //Find all books that has the genre to delete and that has an extra genre
            var booksWithExtraGenres = await _context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.GenresGenre).Where(b => b.BookGenres.Count > 1 && b.BookGenres.Any(bg => bg.GenresGenre.GenreId == id)).ToListAsync();

            //Only look for books that has ONE genre connected that matches the selected genre id
            var booksWithOneGenre = await _context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.GenresGenre).Where(b => b.BookGenres.Count == 1 && b.BookGenres.Any(bg => bg.GenresGenre.GenreId == id)).ToListAsync();

            //Get the first genre out of the genretable (Other)
            var firstGenre = await _context.Genres.FirstOrDefaultAsync();

            //Delete the genre for the books that has an extra genre in the jointable
            foreach (var item in booksWithExtraGenres)
            {
                var remove = _context.BookGenres.Where(x => x.GenresGenreId == id && x.BooksBookId == item.BookId);

                _context.BookGenres.RemoveRange(remove);
                _context.SaveChanges();
            }

            //Update the found books genre to the "Other" value in the database, "Other" genre is the first (Genreid = 1) in the database.
            foreach (var item in booksWithOneGenre)
            {
                //Remove old genre connection
                var olddata = _context.BookGenres.First(x => x.BooksBookId == item.BookId && x.GenresGenreId == id);
                _context.BookGenres.Remove(olddata);
                _context.SaveChanges();

                //Add new genre connection
                BookGenre newdata = new BookGenre { BooksBookId = item.BookId, GenresGenreId = firstGenre.GenreId};
                BookGenre = newdata;
                _context.BookGenres.Add(newdata);
                _context.SaveChanges();
               
            }
            
            var genre = await _context.Genres.SingleAsync(x => x.GenreId == id);

            //Remove selected Genre
            if (genre != null)
            {

                Genre = genre;
                _context.Genres.Remove(Genre);
                await _context.SaveChangesAsync();

            }

            return RedirectToPage("./Index");
        }
    }
}
