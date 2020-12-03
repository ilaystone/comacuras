using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComaCuras.web.Data;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Areas.Panel.Pages.Manager
{
    public class IndexModel : PageModel
    {

        public IActionResult OnGet()
        {
            return RedirectToPage("./Details");
        }
    }
}
