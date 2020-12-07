using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace ComaCuras.web.Areas.Panel.Pages.Agents
{
    public class EditModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;


        public EditModel(ComaCuras.web.Data.ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Agent Agent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Agent = await _context.Agent.FindAsync(id);

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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var agent = await _context.Agent.FindAsync(id);

            if (agent == null)
                return NotFound();

            agent.Name = Agent.Name;
            agent.HolidayEndDate = Agent.HolidayEndDate;

            _context.Attach(agent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(Agent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }
    }
}
