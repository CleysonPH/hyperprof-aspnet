using HyperProf.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperProf.Core.Data.EntityConfigs;

public class TeacherEntityConfig : BaseModelEntityConfig<Teacher>
{
    public override void Configure(EntityTypeBuilder<Teacher> builder)
    {
        base.Configure(builder);

        builder.ToTable("teachers");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Age)
            .HasColumnName("age")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.HourlyPrice)
            .HasColumnName("hourly_price")
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.ProfilePicture)
            .HasColumnName("profile_picture")
            .IsRequired(false)
            .HasMaxLength(255);
    }
}