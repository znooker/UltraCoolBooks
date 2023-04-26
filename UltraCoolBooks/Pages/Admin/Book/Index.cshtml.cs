﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Book
{
    public class IndexModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(SuperCoolBooks.Data.SuperCoolBooksContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [BindProperty, Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public IList<Models.Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.User).ToListAsync();
            }
        }
    }
}
