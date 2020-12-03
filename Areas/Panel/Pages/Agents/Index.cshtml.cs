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

namespace ComaCuras.web.Areas.Panel.Pages.Agents
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

        public IList<Agent> Agent { get;set; }
        public List<List<InputServiceModel>> Service { get; set; } = new List<List<InputServiceModel>>();
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
        public async Task OnGetAsync()
        {
            int v = _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User)).Select(s => s.Id).FirstOrDefault();
            Agent = await _context.Agent
                .Where(a => a.ShopId == v)
                .AsNoTracking().ToListAsync();
            var service = _context.Service
                    .Where(s => s.ShopId == v)
                    .Include(a => a.Shop)
                    .AsNoTracking().ToList();

            foreach (var ag in Agent)
            {
                Service.Add(InputServiceModel.ServiceToInputModel(service, ag.SevicesList));
            }
        }
    }
}
