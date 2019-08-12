using CondoEntryLogs.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

namespace CondoEntryLogs
{
    public class CondoContext : DbContext
    {
        public CondoContext(DbContextOptions<CondoContext> options) : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>().ToTable("Visitor");
        }
    }
}
