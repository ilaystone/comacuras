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
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Areas.Panel.Pages.Services
{
    public class CreateModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;

        public CreateModel(ComaCuras.web.Data.ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Service Service { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int v = await _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User)).Select(s => s.Id).FirstOrDefaultAsync();
            Service.ShopId = v;
            _context.Service.Add(Service);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
