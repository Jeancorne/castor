using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infraestructure.Data.Configuration.Tables
{
    internal class ClsUserConfiguration : IEntityTypeConfiguration<ClsUser>
    {
        public void Configure(EntityTypeBuilder<ClsUser> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50).IsRequired(true);
            builder.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired(true);
            builder.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50).IsRequired(true);
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired(true);
            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
        }
    }
}