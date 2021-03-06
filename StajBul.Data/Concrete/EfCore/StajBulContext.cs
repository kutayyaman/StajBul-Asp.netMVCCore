﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StajBul.Entity;

namespace StajBul.Data.Concrete.EfCore
{
    public class StajBulContext : IdentityDbContext<User, IdentityRole<int>,int>
    {

        public StajBulContext(DbContextOptions<StajBulContext> options) : base(options)
        {}

        public DbSet<InternshipAnnouncement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IdentityRole<int>> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

    }
}
