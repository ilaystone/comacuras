using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Pages.Shops
{
    public class DetailsModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;

        public DetailsModel(ComaCuras.web.Data.ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Shop Shop { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Schedule> Schedule { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Agent> Agents { get; set; }
        public SelectList SelectServicesNames { get; set; }
        [BindProperty]
        public Appointment Appointment { get; set; }
        public string Msg { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, string msg)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shop.FirstOrDefaultAsync(s => s.Id == id);

            if (Shop == null)
            {
                return NotFound();
            }

            Msg = msg;
            Services = await _context.Service.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            Agents = await _context.Agent.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            Schedule = await _context.Schedule.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            SelectServicesNames = new SelectList(Services, nameof(Service.Id), nameof(Service.Name));
            Appointments = await _context.Appointment
                    .Include(a => a.Service)
                    .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                    .Where(a => a.Date.Date >= DateTime.Now.Date)
                    .Where(a => a.ShopId == id)
                    .AsNoTracking()
                    .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shop.FirstOrDefaultAsync(s => s.Id == id);
            Services = await _context.Service.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            Agents = await _context.Agent.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            Schedule = await _context.Schedule.Where(s => s.ShopId == id).AsNoTracking().ToListAsync();
            SelectServicesNames = new SelectList(Services, nameof(Service.Id), nameof(Service.Name));
            Appointments = await _context.Appointment
                    .Include(a => a.Service)
                    .Where(a => string.Equals(a.UserMail, _userManager.GetUserName(User)))
                    .Where(a => a.Date.Date >= DateTime.Now.Date)
                    .Where(a => a.ShopId == id)
                    .AsNoTracking()
                    .ToListAsync();


            if (Shop == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Appointment.UserMail = _userManager.GetUserName(User);
            Appointment.ShopId = (int)id;
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Appointment.ShopId, msg = "Registration success" });
        }
    }
}
