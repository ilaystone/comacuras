using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComaCuras.web.Areas.Identity.Pages.Account;
using ComaCuras.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ComaCuras.web.Areas.Panel.Pages.Admin
{
    [AllowAnonymous]
    public class CreateAdminModel : PageModel
    {
        private readonly SignInManager<ComaCuraswebUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ComaCuraswebUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public CreateAdminModel(
            UserManager<ComaCuraswebUser> userManager,
            SignInManager<ComaCuraswebUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string Msg { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }
        }
        public void OnGet(string msg)
        {
            Msg = msg;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityResult IR;
                var roles = new List<string>() { "admin", "manager", "user" };
                foreach (var r in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(r))
                        IR = await _roleManager.CreateAsync(new IdentityRole(r));
                }
                var user = new ComaCuraswebUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    DOB = Input.DOB,
                    EmailConfirmed = true
                };
                IR = await _userManager.CreateAsync(user, Input.Password);
                if (IR.Succeeded)
                {
                    var u = await _userManager.FindByEmailAsync(user.Email);
                    IR = await _userManager.AddToRoleAsync(u, "admin");
                    if (!IR.Succeeded)
                        return RedirectToPage(new { msg = "registration Failed" });
                    return RedirectToPage(new { msg = "registration success" });
                }
                foreach (var error in IR.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
