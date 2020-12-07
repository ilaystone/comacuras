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
        public DbSet<AgentService> AgentService { get; set; }
        public DbSet<ComaCuras.web.Models.Schedule> Schedule { get; set; }
        public DbSet<ComaCuras.web.Models.Appointment> Appointment { get; set; }
        public DbSet<ComaCuras.web.Models.City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>()
                .HasMany(o => o.Shops)
                .WithOne(o => o.City)
                .IsRequired();


            builder.Entity<Shop>()
                .HasMany(o => o.Services)
                .WithOne(o => o.Shop)
                .IsRequired();
            builder.Entity<Shop>()
                .HasMany(o => o.Agent)
                .WithOne(o => o.Shop)
                .IsRequired();
            builder.Entity<Shop>()
                .HasMany(o => o.Schedule)
                .WithOne(o => o.Shop)
                .IsRequired();
            builder.Entity<Shop>()
                .HasMany(o => o.Rates)
                .WithOne(o => o.Shop)
                .IsRequired();

            builder.Entity<Service>()
                .HasMany(o => o.Appointments)
                .WithOne(o => o.Service)
                .IsRequired();



            builder.Entity<AgentService>()
                .HasKey(o => new { o.AgentId, o.ServiceId });
            builder.Entity<AgentService>()
                .HasOne(o => o.Agent)
                .WithMany(o => o.Services)
                .HasForeignKey(o => o.AgentId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<AgentService>()
                .HasOne(o => o.Service)
                .WithMany(o => o.Agents)
                .HasForeignKey(o => o.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
