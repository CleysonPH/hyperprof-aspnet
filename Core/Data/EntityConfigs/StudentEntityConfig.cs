using HyperProf.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperProf.Core.Data.EntityConfigs;

public class StudentEntityConfig : BaseModelEntityConfig<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.ToTable("students");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ClassDate)
            .HasColumnName("class_date")
            .IsRequired();

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.TeacherId)
            .HasColumnName("teacher_id")
            .IsRequired();
    }
}