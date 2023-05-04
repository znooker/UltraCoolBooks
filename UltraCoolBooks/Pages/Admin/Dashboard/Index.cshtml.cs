using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Models.Review Review { get; set; } = default!;

        public IList<Models.Review> Reviews { get; set; }



        public async Task OnGetAsync()
        {
            Reviews = await _context.Reviews
                .ToListAsync();

         

        }
 

    }

}

