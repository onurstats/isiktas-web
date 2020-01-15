using Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
             : base(options)
        {
        }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
