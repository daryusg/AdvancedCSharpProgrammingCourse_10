using ClubMembershipApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Data
{
    public class ClubMembershipDbContext : DbContext //20260619 Part 5 - Delegates - Create a Code Example
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClubMembershipDb.db");
            optionsBuilder.UseSqlite($"Data Source=ClubMembershipDb.db"); //20260621
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
