using Microsoft.EntityFrameworkCore;
using WebAPIDotNetCore.Domain.Entities;

namespace WebAPIDotNetCore.Domain
{
    public class HPContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        public HPContext(DbContextOptions<HPContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("Character", "HP");
            base.OnModelCreating(modelBuilder);
        }
    }
}
