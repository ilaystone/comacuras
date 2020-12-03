using ComaCuras.web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public static class SuperAdmin
    {
        public static void CreateSuperAdmin(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ComaCuraswebContext>();

            string[] roles = new string[] { "Admin", "Manager", "User" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            var user = new ComaCuraswebUser
            {
                FullName = "Coma Curas",
                Email = "coma.curas@gmail.com",
                NormalizedEmail = "COMA.CURAS@GMAIL.COM",
                UserName = "coma.curas@gmail.com",
                NormalizedUserName = "COMA.CURAS@GMAIL.COM",
                PhoneNumber = "+121000000",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ComaCuraswebUser>();
                var hashed = password.HashPassword(user, "4}s$uyNa*:cN2C.q");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ComaCuraswebUser>(context);
                var result = userStore.CreateAsync(user);
            }

            Task<IdentityResult> rslt = AssignRoles(serviceProvider, user.Email);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email)
        {
            UserManager<ComaCuraswebUser> _userManager = services.GetService<UserManager<ComaCuraswebUser>>();
            ComaCuraswebUser user = await _userManager.FindByEmailAsync(email);
            IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");

            return result;
        }

    }
}
