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
    public class DetailsShopModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public DetailsShopModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public Shop Shop { get; set; }
        public IList<Agent> Agent { get; set; }
        public IList<Schedule> Schedule { get; set; }
        public IList<Service> Service { get; set; }

        public List<List<InputServiceModel>> ServiceInput { get; set; } = new List<List<InputServiceModel>>();
        public class InputServiceModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; } = false;

            public static List<InputServiceModel> ServiceToInputModel(List<Service> service, string chkLst)
            {
                List<InputServiceModel> res = new List<InputServiceModel>();

                foreach (var s in service)
                {
                    res.Add(new InputServiceModel { Id = s.Id, Name = s.Name, IsChecked = (chkLst.Contains(s.Id + ":")) });
                }

                return res;
            }

            public static string ServiceListToString(List<InputServiceModel> lst)
            {
                string res = "";

                foreach (var item in lst)
                {
                    if (item.IsChecked)
                        res += string.Format("{0}:", item.Id);
                }

                return res;
            }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shop
                .Include(s => s.City)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Shop == null)
            {
                return NotFound();
            }
            Agent = await _context.Agent.Where(a => a.ShopId == id).ToListAsync();
            Schedule = await _context.Schedule.Where(s => s.ShopId == id).ToListAsync();
            Service = await _context.Service.Where(s => s.ShopId == id).ToListAsync();
            foreach (var ag in Agent)
            {
                ServiceInput.Add(InputServiceModel.ServiceToInputModel(Service.ToList(), ag.SevicesList));
            }
            return Page();
        }
    }
}
