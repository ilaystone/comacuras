using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;

namespace ComaCuras.web.Areas.Panel.Pages.Admin
{
    public class IndexShopModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public IndexShopModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public PaginatedList<Shop> Shop { get;set; }
        public string CurrentSearch { get; set; }

        public async Task OnGetAsync(string msg, int? pageIndex, string searchString, string currentSearch)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentSearch;
            }
            CurrentSearch = searchString;
            IQueryable<Shop> shopIQ = from s in _context.Shop
                                             select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                shopIQ = shopIQ.Where(s => s.Name.Contains(searchString));
            }
            int pageSize = 10;
            Shop = await PaginatedList<Shop>.CreateAsync(
                shopIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
