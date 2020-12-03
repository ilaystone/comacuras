using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Pages.AjaxCall
{
    public class GetAgentsModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public GetAgentsModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public IList<Agent> Agents { get; set; }

        public async Task OnGetAsync(int srvId)
        {
            int id = _context.Service.Where(s => s.Id == srvId).Select(s => s.ShopId).FirstOrDefault();
            Agents = await _context.Agent.Where(a => a.ShopId == id && a.SevicesList.Contains(srvId + ":")).ToListAsync();
        }
    }
}
