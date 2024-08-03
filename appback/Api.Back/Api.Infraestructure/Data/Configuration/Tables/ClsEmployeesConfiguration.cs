using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infraestructure.Data.Configuration.Tables
{
    internal class ClsEmployeesConfiguration : IEntityTypeConfiguration<ClsEmployees>
    {
        public void Configure(EntityTypeBuilder<ClsEmployees> builder)
        {
            builder.ToTable("employees");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired(true);
            builder.Property(e => e.Identification).HasColumnName("Identification").IsRequired(true);
            builder.Property(e => e.DateRegistry).HasColumnName("DateRegistry").IsRequired(true);
            builder.Property(e => e.IdJobTitle).HasColumnName("IdJobTitle").IsRequired(true);
            builder.Property(e => e.UrlImage).HasColumnName("UrlImage").IsRequired(false);
            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

            builder.HasOne(a => a.IdJobTitleNavigation)
                 .WithMany(e => e.ClsEmployees)
                 .HasForeignKey(a => a.IdJobTitle)
                 .HasConstraintName("fk_ClsEmployees_ClsJobs_IdJobTitle");
        }
    }
}