using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;

namespace ComaCuras.web.Areas.Panel.Pages.Services
{
    public class DeleteModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public DeleteModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service = await _context.Service.FirstOrDefaultAsync(m => m.Id == id);

            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service = await _context.Service.FindAsync(id);

            if (Service != null)
            {
                var Agents = _context.Agent.Where(a => a.ShopId == Service.ShopId).ToList();
                foreach (var item in Agents)
                {
                    if (item.SevicesList.Contains(Service.Id + ":"))
                    {
                        item.SevicesList = item.SevicesList.Replace(Service.Id + ":", "");
                        _context.Attach(item).State = EntityState.Modified;
                    }
                }
                _context.Service.Remove(Service);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
