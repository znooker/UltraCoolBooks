using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Author
{
    public class CreateModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(SuperCoolBooks.Data.SuperCoolBooksContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Author Author { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            long maxFileSizeBytes = 10 * 1024 * 1024; // Max file size (10MB)
            if (Image.Length > maxFileSizeBytes)
            {
                ModelState.AddModelError("Image", $"The uploaded file exceeds the maximum file size of {maxFileSizeBytes / 1024 / 1024} MB.");
                return Page();
            }

            if (Image != null)
            {
                Author.ImagePath = ProcessUploadedFile();
            }

            if (!ModelState.IsValid || _context.Authors == null || Author == null)
            {
                return Page();
            }

            //Check that the uploaded file is a image(png, jpg or jpeg)
            var allowedFileExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(Author.ImagePath); //Gets the file extension of the file
            if (!allowedFileExtensions.Contains(fileExtension)) //Checks if the file does not contain a correct file extension
            {
                ModelState.AddModelError("Author.ImagePath", "File needs to be of type .png, .jpg or .jpeg");
                return Page();
            }


          //Check if an author with the same Name & Birthdate already exists
          if (await _context.Authors.AnyAsync(a=>a.FirstName == Author.FirstName && a.LastName == Author.LastName && a.BirthDate == Author.BirthDate))
            {
                ModelState.AddModelError("Author.FirstName", "An Author with the same Name & Birthdate already exists");
                return Page();
            }

            _context.Authors.Add(Author);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private string ProcessUploadedFile()
        {
            //Check if there is a file to upload
            if (Image != null)
            {
                //Path for the uploads folder
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/author-images");
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
    }
}
