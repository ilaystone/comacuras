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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ComaCuras.web.Pages.Appointments
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

        public IList<Appointment> Appointment { get;set; }
        [BindProperty]
        public SearchInput Query { get; set; }
        public class SearchInput
        {
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
            [Required]
            public int Agent { get; set; }
        }

        public async Task OnGetAsync()
        {
            var agents = from ag in _context.Agent
                         join ap in _context.Appointment
                                        .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                         on ag.Id equals ap.AgentNumber
                         group ap by new { ag.Id, ag.Name } into g
                         select new { Id = g.Key.Id, Name = g.Key.Name };
            ViewData["Agents"] = new SelectList(agents, "Id", "Name");
            Appointment = await _context.Appointment
                .Include(a => a.Service)
                .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                .AsNoTracking().ToListAsync();
        }

        public async Task OnPostAsync()
        {
            var agents = from ag in _context.Agent
                join ap in _context.Appointment
                            .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                on ag.Id equals ap.AgentNumber
                group ap by new { ag.Id, ag.Name } into g
                select new { Id = g.Key.Id, Name = g.Key.Name };
            ViewData["Agents"] = new SelectList(agents, "Id", "Name");
            if (Query.Agent == -1)
            {
                if (Query.Date.Year == 1)
                {
                    Appointment = await _context.Appointment
                        .Include(a => a.Service)
                        .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                        .AsNoTracking().ToListAsync();
                }
                else
                {
                    Appointment = await _context.Appointment
                        .Include(a => a.Service)
                        .Where(a => a.Date.Date == Query.Date.Date)
                        .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                        .AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (Query.Date.Year == 1)
                {
                    Appointment = await _context.Appointment
                        .Include(a => a.Service)
                        .Where(a => a.AgentNumber == Query.Agent)
                        .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                        .AsNoTracking().ToListAsync();
                }
                else
                {
                    Appointment = await _context.Appointment
                        .Include(a => a.Service)
                        .Where(a => a.AgentNumber == Query.Agent)
                        .Where(a => a.Date.Date == Query.Date.Date)
                        .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                        .AsNoTracking().ToListAsync();
                }
            }
        }
    }
}
