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
        public DbSet<OClass> OClass { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<SubjectTeacher> SubjectTeachers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectTeacher>()
             .HasKey(t => new { t.SubjectId, t.TeacherId });


            modelBuilder.Entity<SubjectTeacher>()
                .HasOne<Teacher>(pt => pt.Teacher)
                .WithMany(p => p.SubjectTeachers)
                .HasForeignKey(pt => pt.TeacherId);

            modelBuilder.Entity<SubjectTeacher>()
                .HasOne<Subject>(pt => pt.Subject)
                .WithMany(t => t.SubjectTeachers)
                .HasForeignKey(pt => pt.SubjectId);


        }
    }
}
