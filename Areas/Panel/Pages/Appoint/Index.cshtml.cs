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

namespace ComaCuras.web.Areas.Panel.Pages.Appoint
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
            var sid = await _context.Shop
                .Where(s => s.E_mail == _userManager.GetUserName(User))
                .Select(s => s.Id).FirstOrDefaultAsync();
            ViewData["Agents"] = new SelectList(_context.Agent.Where(a => a.ShopId == sid), "Id", "Name");
            Appointment = await _context.Appointment
                .Where(a => a.ShopId == sid)
                .OrderByDescending(a => a.Date)
                .Include(a => a.Service)
                .AsNoTracking().ToListAsync();
        }

        public async Task OnPostAsync()
        {
            var sid = await _context.Shop
                .Where(s => s.E_mail == _userManager.GetUserName(User))
                .Select(s => s.Id).FirstOrDefaultAsync();
            ViewData["Agents"] = new SelectList(_context.Agent.Where(a => a.ShopId == sid), "Id", "Name");

            IQueryable<Appointment> appointmentIQ = from a in _context.Appointment
                                                  select a;
            appointmentIQ = appointmentIQ.Where(a => a.ShopId == sid);

            if (Query.Date.Date.Year != 1)
            {
                appointmentIQ = appointmentIQ.Where(a => a.Date.Date == Query.Date.Date);
            }
            if (Query.Agent != 0)
            {
                appointmentIQ = appointmentIQ.Where(a => a.AgentNumber == Query.Agent);
            }
            appointmentIQ = appointmentIQ.OrderByDescending(a => a.Date).ThenBy(a => a.Start).ThenBy(a => a.AgentNumber)
                                .Include(a => a.Service)
                                .AsNoTracking();
            Appointment = await appointmentIQ.ToListAsync();
        }

    }
}
