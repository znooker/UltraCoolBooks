using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;

namespace UltraCoolBooks.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly UltraCoolBooks.Data.ApplicationDbContext _context;
        // This interface provides accsess to the current http context which includes info about the request and the user behind the request
        private readonly IHttpContextAccessor _httpContextAccessor;
        // Creates string _userId that can be used within the class
        private readonly string _userId;
        public DetailsModel(UltraCoolBooks.Data.ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            // Tries to get the value of the userid from the authenticated user
            _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public Book Book { get; set; } = default!;

        [BindProperty]
        public Review Review { get; set; }
        public int BookId { get; set; }

        //[BindProperty] binding the property messes up leaving a review.
        public ReviewFeedBack ReviewFeedback { get; set; }

        public List<Review> Reviews { get; set; }
        public List<ReviewFeedBack> ReviewFeedbacks { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.AuthorBooks)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(b => b.GenresGenre)
                .FirstOrDefaultAsync(m => m.BookId == id);

            var reviews = _context.Reviews
                .Include(r => r.User)
                .Where(r => r.BookId == id && r.IsDeleted != true);

            foreach (var review in reviews)
            {
                var timeAgo = GetTimeAgo(review.Created);
                review.CreatedTimeAgo = timeAgo;
            }
            if (reviews.Any())
            {
                var averageRating = reviews.Average(r => r.Rating);
                //This will only take the rating to the nearest decimal
                var formattedRating = averageRating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                ViewData["AverageRating"] = formattedRating;
            }
            else
            {
                ViewData["AverageRating"] = "No rating yet";
            }
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
                Review = new Review { BookId = book.BookId };
                ViewData["Book"] = book;
                Reviews = reviews.ToList();
            }



            return Page();
        }

        //Posting a Review
        public async Task<IActionResult> OnPostAsyncReview()
        {
            // If the user is not logged in send them to login page
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }
            Review.UserId = _userId;


            if (!ModelState.IsValid || Review.ReviewText.Length > 1000 || Review.Title.Length > 255)
            {
                return RedirectToPage("/Books/Details", new { id = Review.BookId });
            }
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            //Redirect to the same page, Return Page(); would not have any data remaining after submitting review
            return RedirectToPage("/Books/Details", new { id = Review.BookId });
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int id)
        {
            // If the user is not logged in send them to login page
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var review = await _context.Reviews.FindAsync(id);
            // Checks if the logged-in user is not the one who wrote the review and is not an admin.
            if (_userId != review.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))
            {
                return Forbid();
            }

            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Books/Details", new { id = review.BookId });
        }
        public async Task<IActionResult> OnPostReviewFeedbackAsync(int id, bool? isHelpful, bool? hasFlagged)
        {
            // If the user is not logged in send them to login page
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id == 0)
            {
                return NotFound();
            }

            // Get the review for which the user is submitting feedback
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }
            // Get the existing review feedback for this review and user, if it exists
            var reviewFeedback = await _context.ReviewFeedBacks
                .SingleOrDefaultAsync(rf => rf.ReviewId == id && rf.UserId == _userId);


            if (reviewFeedback == null)
            {
                // If no review feedback exists, create a new one
                reviewFeedback = new ReviewFeedBack
                {
                    ReviewId = id,
                    UserId = _userId,
                    IsHelpful = isHelpful,
                    HasFlagged = hasFlagged
                };
                // Flagging a review will automatically downvote the review as well
                if (reviewFeedback.HasFlagged == true)
                {
                    review.FLaggedCount++;
                    isHelpful = false;
                }

                // Add the new review feedback to the context
                _context.ReviewFeedBacks.Add(reviewFeedback);

                // Update the Upvotes and Downvotes properties


                if (isHelpful == true)
                {
                    review.Upvotes++;
                }
                else
                {
                    review.Downvotes++;
                }

            }
            else
            {
                // If review feedback already exists, update its IsHelpful property
                bool isHelpfulBefore = reviewFeedback.IsHelpful ?? false;
                reviewFeedback.IsHelpful = isHelpful;

                bool hasFlaggedBefore = reviewFeedback.HasFlagged ?? false;
                reviewFeedback.HasFlagged = hasFlagged;


                // Automatically downvote a review if you flagged it
                if (hasFlaggedBefore == false && hasFlagged == true)
                {
                    review.FLaggedCount++;
                    isHelpful = false;
                }

                // Update the Upvotes and Downvotes properties
                if (isHelpful == true && isHelpfulBefore == false)
                {
                    review.Upvotes++;
                    review.Downvotes--;
                }
                else if (isHelpful == false && isHelpfulBefore == true)
                {
                    review.Downvotes++;
                    review.Upvotes--;
                }


            }

            // Calculate the total number of likes
            review.Likes = review.Upvotes - review.Downvotes;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Details", new { id = review.BookId });
        }
        public async Task<IActionResult> OnPostEditReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            // Checks if the logged-in user is not the one who wrote the review and is not an admin.
            if (_userId != review.UserId && !User.IsInRole("Administrator") && !User.IsInRole("Moderator"))

            {
                return Forbid();
            }
            return RedirectToPage("/Books/EditReview", new { id = review.ReviewId });
        }

        // This method will subtract  the date of review creation with current date and display how long ago the review was created
        // It displays it like youtube does, e.g. a review created 1 50 days ago will display 1 months ago and not 1 month 20 days ago
        private string GetTimeAgo(DateTime date)
        {
            TimeSpan timeAgo = DateTime.Now.Subtract(date);
            // Check if seconds
            if (timeAgo.TotalSeconds < 60)
            {
                return $"{timeAgo.Seconds} seconds ago";
            }
            // Check if minutes
            else if (timeAgo.TotalMinutes < 60)
            {
                return $"{timeAgo.Minutes} minutes ago";
            }
            // Check if hours
            else if (timeAgo.TotalHours < 24)
            {
                return $"{timeAgo.Hours} hours ago";
            }
            // Check if days
            else if (timeAgo.TotalDays < 30)
            {
                return $"{timeAgo.Days} days ago";
            }
            // Check if months
            else if (timeAgo.TotalDays < 365)
            {
                int months = (int)Math.Floor(timeAgo.TotalDays / 30);
                return $"{months} months ago";
            }
            // Check if years
            else if (timeAgo.TotalDays >365)
            {
                int years = (int)Math.Floor(timeAgo.TotalDays / 365);
                return $"{years} years ago";
            }
            return null;
        }
    }
}

