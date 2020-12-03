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
        [BindProperty]
        public List<InputServiceModel> Service { get; set; }
        public Picture Picture { get; set; }
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

            Agent = await _context.Agent.FindAsync(id);

            if (Agent == null)
            {
                return NotFound();
            }

            Picture = await _context.Picture.FindAsync(Agent.PictureId);

            int v = _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User)).Select(s => s.Id).FirstOrDefault();
            var service = _context.Service.Where(s => s.ShopId == v).ToList();
            Service = InputServiceModel.ServiceToInputModel(service, Agent.SevicesList);
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

            agent.SevicesList = InputServiceModel.ServiceListToString(Service);
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
