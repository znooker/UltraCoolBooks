using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using UltraCoolBooks.Data;

namespace UltraCoolBooks.Pages.Admin.User
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UltraCoolUser> _userManager;
        private readonly ApplicationDbContext _context;


        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<UltraCoolUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public string SelectedUserId { get; set; }

        //public SelectList Roles { get; set; }
        [BindProperty, Required, Display(Name = "Role")]
        public string SelectedRole { get; set; }

        [BindProperty]
        public List<UltraCoolUser> Admins { get; set; }

        [BindProperty]
        public List<UltraCoolUser> Moderators { get; set; }

        [BindProperty]
        public List<UltraCoolUser> Users { get; set; }


        public async Task OnGetAsync()
        {

            GetAllTypesOfUsers();

            //Get all options and populate a selectlist
            //GetOptions();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            GetAllTypesOfUsers();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(SelectedUserId);
                await _userManager.AddToRoleAsync(user, SelectedRole);
                
                return RedirectToPage("/Admin/User/Index");
            }
            //return RedirectToPage("/Admin/User/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostDemote()
        {
            GetAllTypesOfUsers();
            //Admins = _userManager.GetUsersInRoleAsync("Administrator").Result.ToList();
            if (Admins.Count <= 1 && SelectedRole == "Administrator")
            {
                ModelState.AddModelError("SelectedRole", $"There must be atleast one admin account!");
                return Page();
                //return RedirectToPage("/Admin/User/Index");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(SelectedUserId);
                await _userManager.RemoveFromRoleAsync(user, SelectedRole);
                //return Page();
                return RedirectToPage("/Admin/User/Index");
            }
            //return RedirectToPage("/Admin/User/Index");
            return Page();
        }

        //public async Task GetOptions()
        //{
        //    var roles = await _roleManager.Roles.ToListAsync();
        //    roles.Add(new IdentityRole { Name = "User", Id = null });
        //    Roles = new SelectList(roles, nameof(IdentityRole.Name));
        //}

        public void GetAllTypesOfUsers()
        {
            
            //Get all user
            var users = _userManager.Users.ToList();

            //Get all admins
            Admins = users.Where(u => _userManager.IsInRoleAsync(u, "Administrator").Result).ToList();

      

            //Get all moderators
            Moderators = users.Where(u => _userManager.IsInRoleAsync(u, "Moderator").Result).ToList();

     
            //Get all users that has no roles
            Users = users.Where(u => _userManager.GetRolesAsync(u).Result.Count == 0).ToList();
           

        }


    }

}



