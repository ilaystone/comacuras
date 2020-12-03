using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.IO;

namespace ComaCuras.web.Areas.Panel.Pages.Admin
{
    public class CreateShopModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;
        public CreateShopModel(ComaCuras.web.Data.ComaCuraswebContext context,
                                    UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["Cities"] = new SelectList(_context.Cities.OrderBy(a => a.Name), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Shop Shop { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ComaCuraswebUser
            {
                UserName = Shop.E_mail,
                Email = Shop.E_mail,
                FullName = Shop.Name,
                DOB = DateTime.Now.AddYears(-20),
                EmailConfirmed = true
            };

            var password = "hinata@ANGEWO1"; // to change afterwards

            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if (userExists != null)
                return RedirectToPage(new { msg = "Email Already Exits" });
            IdentityResult IR = await _userManager.CreateAsync(user, password);
            if (IR.Succeeded)
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                IR = await _userManager.AddToRoleAsync(u, "manager");
                if (!IR.Succeeded)
                    return RedirectToPage(new { msg = "registration Failed" });
                _context.Shop.Add(Shop);
                await _context.SaveChangesAsync();
                Create_schedule(Shop.Id);
                return RedirectToPage("/Admin/IndexShop", new { msg = "registration success" });
            }
            return RedirectToPage(new { msg = "registration failed" });
        }

        private void Create_schedule(int id)
        {
            List<Schedule> schedule = new List<Schedule>();
            for (int i = 1; i < 8; i++)
            {
                schedule.Add(new Schedule()
                {
                    Day = i,
                    Start = "00:00",
                    End = "00:00",
                    Pstart = "00:00",
                    Pend = "00:00",
                    ShopId = id
                });
            }
            schedule.ForEach(n => _context.Schedule.Add(n));
            _context.SaveChanges();
        }
    }
}
