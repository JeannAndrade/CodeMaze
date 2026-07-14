using LumiaFoundation.EFRepository.Domain;

namespace Entities.Models
{
    public class Company : Entity
    {

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}