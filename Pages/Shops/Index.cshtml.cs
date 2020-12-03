using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComaCuras.web.Pages.Shops
{
    public class IndexModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public IndexModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public PaginatedList<Shop> Shop { get;set; }
        public string NameSort { get; set; }
        public string LocalSort { get; set; }
        public string UserLocal { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentSearch { get; set; }
        public int CityIndex { get; set; }
        [BindProperty]
        public int SelectedTag { get; set; } 

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, string userLocal, int c)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            LocalSort = String.Equals(sortOrder, "local_asc") ? "local_desc" : "local_asc";
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "Name");
            if (!string.IsNullOrEmpty(userLocal))
                UserLocal = userLocal;
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = CurrentSearch;
            CurrentSearch = searchString;
            var shops = from s in _context.Shop
                        select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                shops = shops.Where(s => s.Name.Contains(searchString));
            }
            SelectedTag = c;
            if (c > 0)
            {
                shops = shops.Where(s => s.CityId == c);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shops = shops.OrderByDescending(s => s.Name);
                    break;
                case "local_asc":
                    foreach (var item in shops)
                    {
                        item.Location = Distance_from(item.Location);
                    }
                    shops = shops.OrderBy(s => s.Location);
                    break;
                case "local_desc":
                    foreach (var item in shops)
                    {
                        item.Location = Distance_from(item.Location);
                    }
                    shops = shops.OrderByDescending(s => s.Location);
                    break;
                default:
                    shops = shops.OrderBy(s => s.Name);
                    break;
            }
            shops = shops.Where(s => s.SubscriptionEndDate.Date > DateTime.Now.Date);
            shops = shops.Where(s => s.HolidayEndDate.Date < DateTime.Now.Date);
            int pageSize = 3;
            Shop = await PaginatedList<Shop>.CreateAsync(
                shops , pageIndex ?? 1, pageSize);
        }


        /*
         * ditance based on harvesine equation
         */
        private string Distance_from(string loc)
        {
            List<double> a = new List<double>();
            List<double> b = new List<double>();
            UserLocal.Split(":").ToList().ForEach(item => a.Add(Convert.ToDouble(item.Replace('.', ','))));
            loc.Split(":").ToList().ForEach(item => b.Add(Convert.ToDouble(item.Replace('.', ','))));
            var R = 6371e3; // metres
            var s1 = a[0] * Math.PI / 180;
            var s2 = b[0] * Math.PI / 180;
            var ds = (b[0] - a[0]) * Math.PI / 180;
            var dg = (b[1] - a[1]) * Math.PI / 180;

            var an = Math.Sin(ds / 2) * Math.Sin(ds / 2) +
                    Math.Cos(s1) * Math.Cos(s2) * Math.Sin(dg / 2) * Math.Sin(dg / 2);
            var cn = 2 * Math.Atan2(Math.Sqrt(an), Math.Sqrt(1 - an));
            return (R * cn * 1000).ToString();
        }
    }
}
