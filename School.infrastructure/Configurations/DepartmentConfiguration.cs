using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            _ = builder.HasKey(x => x.DID);
            _ = builder.Property(x => x.DName).HasMaxLength(500);

            _ = builder.HasMany(x => x.Students)
                  .WithOne(x => x.Department)
                  .HasForeignKey(x => x.DID)
                  .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasOne(x => x.Instructor)
            .WithOne(x => x.departmentManager)
            .HasForeignKey<Department>(x => x.InsManager)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
