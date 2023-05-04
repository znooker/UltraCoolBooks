using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Book
{
    [Authorize(Policy = "AdminPolicy")]
    public class EditModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(UltraCoolBooks.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Models.Book Book { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> Authors { get; set; }

        [BindProperty]
        public List<int> GenreId { get; set; }
        [BindProperty]
        public int AuthorId { get; set; }

        [BindProperty]
        public List<SelectListItem> Genres { get; set; }

        [BindProperty]
        public List<Models.Genre> OldGenres { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

      

        public async Task<IActionResult> OnGetAsync(int? id)
        {


            if (id == null || _context.Books == null)
            {
                return NotFound();
            }


            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;

            PopulateOptionLists();

            //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //IMG Upload
            long maxFileSizeBytes = 10 * 1024 * 1024; // Max file size (10MB)
            if (Image.Length > maxFileSizeBytes)
            {
                ModelState.AddModelError("Image", $"The uploaded file exceeds the maximum file size of {maxFileSizeBytes / 1024 / 1024} MB.");
                return Page();
            }

            if (Image != null)
            {
                Book.ImagePath = ProcessUploadedFile();
            }

            //NOT FINISHED!!!! - Set User who Created the book, set the logged in user as the creator - NOT FINISHED!!!!
            Book.UserId = "1"; //Hardcoded value
            Book.isDeleted = false;


            if (!ModelState.IsValid)
            {
                return Page();
            }


            //Check that the uploaded file is a image(png, jpg or jpeg)
            var allowedFileExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(Book.ImagePath); //Gets the file extension of the file
            if (!allowedFileExtensions.Contains(fileExtension)) //Checks if the file does not contain a correct file extension
            {
                ModelState.AddModelError("Image", "File needs to be of type .png, .jpg or .jpeg");

                //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

                PopulateOptionLists();

                return Page();
            }

            //Check is same ISBN already exists and bookid not equal to this books id
            if (await _context.Books.AnyAsync(a => a.ISBN == Book.ISBN && a.BookId != Book.BookId))
            {
                ModelState.AddModelError("Book.ISBN", "A Book with that ISBN already exists!");

                //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

                PopulateOptionLists();

                return Page();
                //return RedirectToPage("/Books/Details", new { id = review.BookId });
                //return RedirectToPage(Book);
            }



            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Find Author in the selected to the book
            var authors = _context.Authors.
                Include(author => author.AuthorBooks).
                ThenInclude(ab => ab.BooksBook).
                Where(author => author.AuthorBooks.
                Any(ab => ab.BooksBookId == Book.BookId)).ToList();

            //Remove previous Authors connected to the book
            foreach (var item in authors)
            {
                var oldAuthors = _context.AuthorBooks.First(x => x.AuthorId == item.AuthorId && x.BooksBookId == Book.BookId);
                _context.Remove(oldAuthors);
                _context.SaveChanges();

            }

            //Find Genres in the selected book
            var genres = _context.Genres.
               Include(bg => bg.BookGenres).
               Where(bg => bg.BookGenres.
               Any(b => b.BooksBookId == Book.BookId)).ToList();


            //Remove previous Genres
            foreach (var item in genres)
            {
                var oldGenre = _context.BookGenres.First(g => g.GenresGenreId == item.GenreId && g.BooksBookId == Book.BookId);
                _context.Remove(oldGenre);
                _context.SaveChanges();
            }


            //Add new selected Author
            AuthorBook authorBook = new AuthorBook() { AuthorId = AuthorId, BooksBookId = Book.BookId };
            _context.AuthorBooks.Add(authorBook);

            //Add new selected Genres
            List<BookGenre> glist = new List<BookGenre>();
            foreach (var id in GenreId)
            {
                glist.Add(new BookGenre { BooksBookId = Book.BookId, GenresGenreId = id });
            }

            _context.BookGenres.AddRange(glist);
            await _context.SaveChangesAsync();



            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }

        private string ProcessUploadedFile()
        {
            //Check if there is a file to upload
            if (Image != null)
            {
                //Path for the uploads folder
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/book-images");
                //File name of the uploaded file
                string fileName = Path.GetFileName(Image.FileName);
                //combines the uploads folder and filename to create a path to the file
                string filePath = Path.Combine(uploadsFolder, fileName);
                //open a file stream to the destination file and copy the uploaded file to it
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                //return the filename which will be used for Author.ImagePath
                return fileName;
            }
            //if no file to upload
            return null;
        }


        private void PopulateOptionLists()
        {
            Authors =  _context.Authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();

            //Genres in the selected book
            var genres = _context.Genres.
               Include(bg => bg.BookGenres).
               Where(bg => bg.BookGenres.
               Any(b => b.BooksBookId == Book.BookId)).ToList();

            OldGenres = genres;

            Genres =  _context.Genres.Include(g => g.BookGenres).ThenInclude(bg => bg.BooksBook).Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Title,
                Selected = g.BookGenres
             .Any(bg => bg.BooksBookId == Book.BookId && bg.GenresGenre.GenreId == g.GenreId)
            }).ToList();





        }

    }
}
