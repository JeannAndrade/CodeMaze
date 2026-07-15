using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(k => k.Id);
            builder.Property(e => e.Id).HasColumnName("CompanyId");
            builder.Property(e => e.Name).HasMaxLength(60).IsRequired();
            builder.Property(e => e.Address).HasMaxLength(60).IsRequired();
            builder.Property(e => e.Country).HasMaxLength(60);
        }
    }
}