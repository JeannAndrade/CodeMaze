using LumiaFoundation.EFRepository.Domain;

namespace Entities.Models
{
    public class Employee : Entity
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}