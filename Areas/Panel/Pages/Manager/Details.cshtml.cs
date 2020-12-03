using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Identity;

namespace ComaCuras.web.Areas.Panel.Pages.Manager
{
    public class DetailsModel : PageModel
    {
        private readonly ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;

        public DetailsModel(ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Shop Shop { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Shop = await _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User))
                            .Include(c => c.City)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            if (Shop == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
