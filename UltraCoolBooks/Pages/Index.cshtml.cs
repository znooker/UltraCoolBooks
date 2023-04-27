using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages
{
    public class IndexModel : PageModel
    {



    
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Book> Books { get; set; } = default!;
        public Book RandomBook { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                if (_context.Books != null)
                {
                    Books = await _context.Books
                    .Where(e => e.isDeleted == false)
                    .Include(e => e.AuthorBooks)
                    .ThenInclude(e => e.Author)
                    .ToListAsync();

                    //Get a list of all bookId
                    var bookIdsList = await _context.Books
                     .Where(e => e.isDeleted == false)
                     .Select(e => e.BookId)
                     .ToListAsync();

                    var random = new Random();
                    var count = bookIdsList.Count;
                    var index = random.Next(count);

                    if (count > 0)
                    { 
                        RandomBook = Books[index];
                    }
                }
            }

            else 
            {
                Books = await _context.Books
                     .Where(e => e.isDeleted == false)
                   .Where(e => e.Title.Contains(SearchString) ||
                            e.AuthorBooks.Any(e => e.Author.FirstName.Contains(SearchString)
                            || e.Author.LastName.Contains(SearchString))
                            ||e.BookGenres.Any(e => e.GenresGenre.Title.Contains(SearchString)))
                  .Include(e => e.BookGenres)
                  .ThenInclude(e => e.GenresGenre)
                  .Include(e => e.AuthorBooks)
                  .ThenInclude(e => e.Author)
                  .ToListAsync();
            }

        }

   
    }




}

