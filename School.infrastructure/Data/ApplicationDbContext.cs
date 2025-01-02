using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Entities.Identity;
using School.Data.Entities.Views;
using System.Reflection;

namespace School.infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        //private readonly IEncryptionProvider _encryptionProvider;
        public ApplicationDbContext()
        {
            // _encryptionProvider = new GenerateEncryptionProvider("d78a5c1113954a90bf81beab71bba32acfb5d0df22a54e00bc2c2fb142db72f7");

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ////  _encryptionProvider = new GenerateEncryptionProvider("d78a5c1113954a90bf81beab71bba32acfb5d0df22a54e00bc2c2fb142db72f7");
        }
        public DbSet<AppUser> Appuser { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<UserRefershToken> UserRefershToken { get; set; }
        #region Views
        public DbSet<DepartmentView> DepartmentView { get; set; }
        #endregion
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
            // modelBuilder.UseEncryption(_encryptionProvider);
        }

    }
}
