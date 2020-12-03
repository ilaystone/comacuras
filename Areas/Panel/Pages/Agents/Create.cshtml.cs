using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Areas.Panel.Pages.Agents
{
    public class CreateModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;
        private readonly UserManager<ComaCuraswebUser> _userManager;

        public CreateModel(ComaCuras.web.Data.ComaCuraswebContext context, UserManager<ComaCuraswebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            int v = _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User)).Select(s => s.Id).FirstOrDefault();
            var service = _context.Service.Where(s => s.ShopId == v).ToList();
            Service = InputServiceModel.ServiceToInputModel(service);
            return Page();
        }

        [BindProperty]
        public Agent Agent { get; set; }
        [BindProperty]
        public List<InputServiceModel> Service { get; set; }

        public class InputServiceModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; } = false;

            public static List<InputServiceModel> ServiceToInputModel(List<Service> service)
            {
                List<InputServiceModel> res = new List<InputServiceModel>();
                foreach (var s in service)
                {
                    res.Add(new InputServiceModel { Id = s.Id, Name = s.Name });
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int v = await _context.Shop.Where(s => s.E_mail == _userManager.GetUserName(User)).Select(s => s.Id).FirstOrDefaultAsync();
            Agent.ShopId = v;
            Agent.SevicesList = InputServiceModel.ServiceListToString(Service);
            _context.Agent.Add(Agent);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
