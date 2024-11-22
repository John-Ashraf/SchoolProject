using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.infrastructure.Configurations
{
    public class Ins_SubjectConfigurations : IEntityTypeConfiguration<Ins_Subject>
    {
        public void Configure(EntityTypeBuilder<Ins_Subject> builder)
        {
            _ = builder
                .HasKey(x => new { x.SubId, x.InsId });


            _ = builder.HasOne(ds => ds.instructor)
                     .WithMany(d => d.Ins_Subjects)
                     .HasForeignKey(ds => ds.InsId);

            _ = builder.HasOne(ds => ds.Subject)
                 .WithMany(d => d.Ins_Subjects)
                 .HasForeignKey(ds => ds.SubId);

        }
    }
}
