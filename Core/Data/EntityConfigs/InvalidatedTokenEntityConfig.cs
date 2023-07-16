using HyperProf.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperProf.Core.Data.EntityConfigs;

public class InvalidatedTokenEntityConfig : BaseModelEntityConfig<InvalidatedToken>
{
    public override void Configure(EntityTypeBuilder<InvalidatedToken> builder)
    {
        base.Configure(builder);

        builder.ToTable("invalidated_tokens");

        builder.Property(x => x.Token)
            .HasColumnName("token")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();
    }
}