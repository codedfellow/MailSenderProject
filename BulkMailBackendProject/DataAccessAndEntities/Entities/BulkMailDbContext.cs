using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessAndEntities.Entities
{
    public class BulkMailDbContext : DbContext
    {
        public BulkMailDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User>? User { get; set; }
        public DbSet<EmailLog>? EmailLog { get; set; }
        public DbSet<ScheduledMail>? ScheduledMail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToCollection("User");
            modelBuilder.Entity<EmailLog>().ToCollection("EmailLog");
            modelBuilder.Entity<ScheduledMail>().ToCollection("ScheduledMail");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
