using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {

        private static readonly List<Company> Companies =
        [
            new()
            {
                Name = "IT_Solutions Ltd",
                Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                Country = "USA"
            },
            new()
            {
                Name = "Admin_Solutions Ltd",
                Address = "312 Forest Avenue, BF 923",
                Country = "USA"
            }
        ];

        public static List<Company> GetCompanies() => Companies;

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(k => k.Id);
            builder.Property(e => e.Id).HasColumnName("CompanyId");
            builder.Property(e => e.Name).HasMaxLength(60).IsRequired();
            builder.Property(e => e.Address).HasMaxLength(60).IsRequired();
            builder.Property(e => e.Country).HasMaxLength(60);

            builder.HasData(Companies);
        }
    }
}