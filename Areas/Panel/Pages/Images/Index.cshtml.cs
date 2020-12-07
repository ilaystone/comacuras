using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Data;
using ComaCuras.web.Models;

namespace ComaCuras.web.Areas.Panel.Pages.Images
{
    public class IndexModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public IndexModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public List<string> ImageUrl { get; set; } = new List<string>();

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();
            return Page();
        }
    }
}
