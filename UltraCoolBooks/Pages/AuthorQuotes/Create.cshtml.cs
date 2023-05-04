using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.AuthorQuotes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;
        // This interface provides accsess to the current http context which includes info about the request and the user behind the request
        private readonly IHttpContextAccessor _httpContextAccessor;
        // Creates string _userId that can be used within the class
        private readonly string _userId;

        public CreateModel(UltraCoolBooks.Data.ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            // Tries to get the value of the userid from the authenticated user
            _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "FullName");
            return Page();
        }

        [BindProperty]
        public Models.AuthorQuote AuthorQuote { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            AuthorQuote.UserId = _userId;
            AuthorQuote.IsAccepeted = false;

            if (!ModelState.IsValid || AuthorQuote.QuoteText.Length > 255)
            {
                return Page();
            }

            _context.AuthorQuote.Add(AuthorQuote);
            await _context.SaveChangesAsync();
            // This TempData gets sent to the Index page model
            TempData["SubmitConfirmation"] = "Thank you, your quote is awaiting review from a moderator";
            return RedirectToPage("./Index");
        }
    }
}
