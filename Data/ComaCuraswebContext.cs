using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ComaCuras.web.Data
{
    public class ComaCuraswebContext : IdentityDbContext<ComaCuraswebUser>
    {
        public ComaCuraswebContext(DbContextOptions<ComaCuraswebContext> options)
            : base(options)
        {
        }

        public DbSet<ComaCuras.web.Models.Shop> Shop { get; set; }
        public DbSet<ComaCuras.web.Models.Service> Service { get; set; }
        public DbSet<ComaCuras.web.Models.Agent> Agent { get; set; }
        public DbSet<ComaCuras.web.Models.Schedule> Schedule { get; set; }
        public DbSet<ComaCuras.web.Models.Appointment> Appointment { get; set; }
        public DbSet<ComaCuras.web.Models.Picture> Picture { get; set; }
        public DbSet<ComaCuras.web.Models.City> Cities { get; set; }
    }
}
