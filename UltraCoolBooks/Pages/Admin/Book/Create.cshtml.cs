﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;


namespace UltraCoolBooks.Pages.Admin.Book
{
    [Authorize(Policy = "AdminPolicy")]
    public class LabCreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UltraCoolUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LabCreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<UltraCoolUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public IFormFile Image { get; set; }


        public async Task<IActionResult> OnGet()
        {
            //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

            PopulateOptionLists();

            return Page();

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Models.Book book)
        {

            //Hur funkar magin här???!?!"#!¤!¤%"#¤!!¤&"#&¤"
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var testid = user.Id;

            var test = Request.Form["testAId"];

            //IMG Upload
            long maxFileSizeBytes = 10 * 1024 * 1024; // Max file size (10MB)
            if (Image.Length > maxFileSizeBytes)
            {
                ModelState.AddModelError("Image", $"The uploaded file exceeds the maximum file size of {maxFileSizeBytes / 1024 / 1024} MB.");
                return Page();
            }




            //NOT FINISHED!!!! - Set User who Created the book, set the logged in user as the creator - NOT FINISHED!!!!
            Book.UserId = user.Id; //Hardcoded value
            Book.ImagePath = GetFileExtension();

            if (!ModelState.IsValid || Book.Title.Length > 255 || Book.Description.Length > 1000 || Book.ISBN.Length > 20)
            {
                //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

                PopulateOptionLists();

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



            //Check is same ISBN already exists
            if (await _context.Books.AnyAsync(a => a.ISBN == Book.ISBN))
            {
                ModelState.AddModelError("Book.ISBN", "A Book with that ISBN already exists!");

                //ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

                PopulateOptionLists();

                return Page();
                //return RedirectToPage("/Books/Details", new { id = review.BookId });
                //return RedirectToPage(Book);
            }

            if (Image != null)
            {
                Book.ImagePath = ProcessUploadedFile();
            }

            _context.Books.Add(Book);

            await _context.SaveChangesAsync();

            //Add connection between book and authors in the JoinTable AuthorBook
            AuthorBook authorBook = new AuthorBook() { AuthorId = AuthorId, BooksBookId = Book.BookId };
            _context.AuthorBooks.Add(authorBook);

            List<BookGenre> glist = new List<BookGenre>();
            foreach (var id in GenreId)
            {
                glist.Add(new BookGenre { BooksBookId = Book.BookId, GenresGenreId = id });
            }

            _context.BookGenres.AddRange(glist);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }

        private void PopulateOptionLists()
        {
            Authors = _context.Authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();

            Genres = _context.Genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = $"{g.Title}"
            }).ToList();
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

        private string GetFileExtension()
        {
            //Check if there is a file to upload
            if (Image != null)
            {
                string fileName = Path.GetFileName(Image.FileName);
                return fileName;
            }
            return null;
        }

    }
}
