using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Models;
using System.ComponentModel.DataAnnotations;
using Syncfusion.EJ2.Diagrams;

namespace UltraCoolBooks.Pages.Admin
{
	public class dashboardModel : PageModel
	{

		[BindProperty]
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

		public dashboardModel(UltraCoolBooks.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public class ReviewCount
		{
			public DateTime Date { get; set; }
			public int Count { get; set; }
		}
		public IList<ReviewCount> ReviewCounts { get; set; } = default!;
		public IList<Review> Reviews { get; set; } = default!;
		public IList<int> Counts { get; set; } = default!;



		public Data.ApplicationDbContext Get_context()
		{
			return _context;
		}


		public async Task GetReviews()
		{
			List<ReviewCount> ReviewCounts;
			if (StartDate.Year > 0)
			{
				ReviewCounts = await _context.Reviews
					.Where(r => r.Created >= StartDate && r.Created <= EndDate)
					.GroupBy(r => r.Created.Date)
					.Select(g => new ReviewCount { Date = g.Key, Count = g.Count() })
					.OrderBy(r => r.Date)
					.ToListAsync();
			}
			else
			{
				ReviewCounts = await _context.Reviews
					.GroupBy(r => r.Created.Date)
					.Select(g => new ReviewCount { Date = g.Key, Count = g.Count() })
					.OrderBy(r => r.Date)
					.ToListAsync();
			}


			List<int> Counts = new List<int>();
			foreach (var reviewCount in ReviewCounts)
			{
				Counts.Add(reviewCount.Count);
			}

		}




	}
}