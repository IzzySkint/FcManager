using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FcManager.Data
{
	public class FcManagerDbContext : IdentityDbContext
    {
        private readonly string _connectionString = "Data Source=../FcManager/db/fcmanager.sqlite";
        
		public FcManagerDbContext()
            : base()
		{
		}

        public FcManagerDbContext(DbContextOptions options)
            : base(options)

        {
        }

        public FcManagerDbContext(string connectionString) 
            : base()
        {
            _connectionString = connectionString;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Stadium>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Province>().HasIndex(c => c.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionString);
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}

