using LumiaFoundation.EFRepository.Domain;

namespace Entities.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}