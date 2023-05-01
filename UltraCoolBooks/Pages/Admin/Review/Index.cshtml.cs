using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Admin.Review
{
    public class IndexModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public UltraCoolBooks.Models.Book Book { get; set; }
        public IList<Models.Review> Reviews { get; set; }
        public List<ReviewFeedBack> ReviewFeedbacks { get; set; }
        public async Task OnGetAsync()
        {
            var flaggedReviews = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.IsDeleted != true && r.FLaggedCount > 0)
                .OrderBy(r => r.FLaggedCount)
                .ToListAsync();

            Reviews = flaggedReviews;
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/Review/Index");
        }
    }
}
