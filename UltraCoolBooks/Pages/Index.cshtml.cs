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
        public string SelectedGenre { get; set; }
        public string SelectedAuthor { get; set; }

        public IList<Models.Book> BooksList { get; set; } = default!;
        public IList<Models.Genre> Genres { get; set; } = default!;
        public IList<Models.Author> Authors { get; set; } = default!;

        public async Task OnGetAsync(string selectedGenre, string selectedAuthor)
        {
            Genres = await _context.Genres.ToListAsync();
            Authors = await _context.Authors.ToListAsync();
            if (string.IsNullOrEmpty(SearchString))
            {
                if (_context.Books != null)
                {
                    // Get all books (not deleted)
                    Books = await _context.Books
                    .Where(e => e.isDeleted == false)
                    .Include(e => e.BookGenres)
                    .ThenInclude(e => e.GenresGenre)
                    .Include(e => e.AuthorBooks)
                    .ThenInclude(e => e.Author)
                    .ToListAsync();



                    List<Book> selectedObjects = new List<Book>(); // skapa en tom lista för de valda objekten

                    if (selectedAuthor != null)
                    {
                        int authorNum = int.Parse(selectedAuthor);

                        foreach (var item in Books)
                        {
                            foreach (var item2 in item.AuthorBooks)

                                if (item2.AuthorId == authorNum && !selectedObjects.Contains(item)) // kolla om objektet uppfyller vårt kriterium
                                {
                                    selectedObjects.Add(item); // lägg till objektet i den nya listan om det är valt
                                }
                        }
                    }

                    if (selectedGenre != null)
                    {
                        int genreNum = int.Parse(selectedGenre);

                        foreach (var item in Books)
                        {
                            foreach (var item2 in item.BookGenres)

                                if (item2.GenresGenreId == genreNum && !selectedObjects.Contains(item)) // kolla om objektet uppfyller vårt kriterium
                                {
                                    selectedObjects.Add(item); // lägg till objektet i den nya listan om det är valt
                                }
                        }
                    }
                    if (selectedObjects.Count > 0)
                    {
                        Books = selectedObjects;
                    }

                    //Random book
                    if ((string.IsNullOrEmpty(SearchString)) && (string.IsNullOrEmpty(selectedGenre)) && (string.IsNullOrEmpty(selectedAuthor)))
                    {
                        var bookIds = await _context.Books
                        .Where(e => e.isDeleted == false)
                        .Select(e => e.BookId)
                        .ToListAsync();

                        var random = new Random();
                        var bookIdsList = await _context.Books
                                            .Where(e => e.isDeleted == false)
                                            .Select(e => e.BookId)
                                            .ToListAsync();

                        if (bookIds.Count != 0)
                        {


                            var randomIndex = random.Next(bookIds.Count);

                            if (randomIndex >= 0)
                            {


                                var randomBookId = bookIds[randomIndex];


                                RandomBook = _context.Books
                                     .Where(e => e.BookId == randomBookId)
                                    .Include(e => e.AuthorBooks)
                                    .ThenInclude(e => e.Author)
                                     .Include(e => e.BookGenres)
                                    .ThenInclude(e => e.GenresGenre)
                                    .FirstOrDefault();
                            }
                        }        

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

