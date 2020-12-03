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

namespace ComaCuras.web.Areas.Panel.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;


        public IndexModel(ComaCuras.web.Data.ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Schedule> Schedule { get;set; }

        public async Task OnGetAsync()
        {
            var Shop = await _context.Shop
                .Where(s => s.E_mail == _userManager.GetUserName(User))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            Schedule = await _context.Schedule.Where(s => Shop.Id == s.ShopId)
                .Include(s => s.Shop).ToListAsync();
        }
    }
}
