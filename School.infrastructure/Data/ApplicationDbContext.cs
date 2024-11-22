using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using System.Reflection;

namespace School.infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<DepartmetSubject>().HasKey(x => new { x.SubID, x.DID });
            _ = modelBuilder.Entity<StudentSubject>().HasKey(x => new { x.SubID, x.StudID });
            _ = modelBuilder.Entity<Ins_Subject>().HasKey(x => new { x.InsId, x.SubId });
            _ = modelBuilder.Entity<Instructor>().HasOne(x => x.Supervisor)
                .WithMany(x => x.Instructors).HasForeignKey(x => x.SupervisorId).OnDelete(DeleteBehavior.Restrict);
            _ = modelBuilder.Entity<Department>().HasOne(x => x.Instructor)
               .WithOne(x => x.departmentManager).HasForeignKey<Department>(x => x.InsManager).OnDelete(DeleteBehavior.Restrict);

            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
