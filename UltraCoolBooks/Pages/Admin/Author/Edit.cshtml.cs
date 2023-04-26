using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Author
{
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
        public Models.Author Author { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }


            var author =  await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            Author = author;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            //Check that the uploaded file is a image(png, jpg or jpeg)
            var allowedFileExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(Author.ImagePath); //Gets the file extension of the file
            if (!allowedFileExtensions.Contains(fileExtension)) //Checks if the file does not contain a correct file extension
            {
                ModelState.AddModelError("Author.ImagePath", "File needs to be of type .png, .jpg or .jpeg");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }




            _context.Attach(Author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.AuthorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _context.Update(Author);
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

        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
