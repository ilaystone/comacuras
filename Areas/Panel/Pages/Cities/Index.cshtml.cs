using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;

namespace ComaCuras.web.Areas.Panel.Pages.Cities
{
    public class IndexModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public IndexModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public PaginatedList<City> City { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<City> cityIQ = from c in _context.Cities
                                      select c;
            int pageSize = 10;
            City = await PaginatedList<City>.CreateAsync(
                cityIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
