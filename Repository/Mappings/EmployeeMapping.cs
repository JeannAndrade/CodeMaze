using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(k => k.Id);
            builder.Property(e => e.Id).HasColumnName("EmployeeId");
            builder.Property(e => e.Name).HasMaxLength(30).IsRequired();
            builder.Property(e => e.Age).IsRequired();
            builder.Property(e => e.Position).HasMaxLength(20).IsRequired();
            builder.Property(e => e.CompanyId).HasColumnName("CompanyId");

            builder
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}