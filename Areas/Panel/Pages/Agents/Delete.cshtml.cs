using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;

namespace ComaCuras.web.Areas.Panel.Pages.Agents
{
    public class DeleteModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public DeleteModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Agent Agent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Agent = await _context.Agent
                .Include(a => a.Shop).FirstOrDefaultAsync(m => m.Id == id);

            if (Agent == null)
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

            Agent = await _context.Agent.FindAsync(id);

            if (Agent != null)
            {
              
            }

            return RedirectToPage("./Index");
        }
    }
}
