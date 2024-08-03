using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infraestructure.Data.Configuration.Tables
{
    internal class ClsJobsConfiguration : IEntityTypeConfiguration<ClsJobs>
    {
        public void Configure(EntityTypeBuilder<ClsJobs> builder)
        {
            builder.ToTable("jobs");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired(true);
            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
        }
    }
}