using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UltraCoolBooks.Data;
using UltraCoolBooks.Models;
namespace UltraCoolBooks.Pages.Admin.Dashboard
{
    public class IndexModel : PageModel

    {

		private readonly UltraCoolBooks.Data.ApplicationDbContext _context;

        public IndexModel(UltraCoolBooks.Data.ApplicationDbContext context)
        {
            _context = context;
        }
		public DateTime startDate = DateTime.Now.AddDays(-7).Date;
		public DateTime endDate = DateTime.Now;
		public List<DateTime> dates = new List<DateTime>();
		
		public List<int> reviewCount = new List<int>();
		public List<Models.Review> Reviews = new List<Models.Review>();

		public void OnGet()
		{
			//LoadPage();
			getStats();
		}

		public void OnPostDateSelect(string sd, string ed)
		{
			startDate = DateTime.Parse(sd);
			endDate = DateTime.Parse(ed);

		
		}

		public string DateToString1(DateTime? d) //also cut off time
		{
			return String.Format($"{d:yyyy-MM-dd}");
		}


		public void getStats()
		{
			var query = _context.Reviews.AsQueryable();
			query = query.Where(x => x.Created >= startDate && x.Created <= endDate);
			query = query.OrderBy(x => x.Created);
			Reviews = query.ToList();

			for (DateTime d = startDate.Date; d != endDate.Date; d = d.AddDays(1))
			{
				int count = 0;
				dates.Add(d);
				foreach (var r in Reviews) //TODO inefficient?
				{
					if (DateToString1(r.Created) == DateToString1(d)) count++;
				}
				reviewCount.Add(count);
			}
		}
		
	

			//for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
			//{
			//	dates.Add(date); // Här fylls listan på med alla datum i valt spann
			//}

		



	}

}

