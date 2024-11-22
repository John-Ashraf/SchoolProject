using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.infrastructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            _ = builder.HasKey(x => new { x.SubID, x.DID });

            _ = builder.HasOne(ds => ds.Department)
                 .WithMany(d => d.DepartmentSubjects)
                 .HasForeignKey(ds => ds.DID);

            _ = builder.HasOne(ds => ds.Subject)
                 .WithMany(d => d.DepartmetsSubjects)
                 .HasForeignKey(ds => ds.SubID);


        }
    }
}
