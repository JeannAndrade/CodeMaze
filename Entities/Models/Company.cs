using LumiaFoundation.EFRepository.Domain;

namespace Entities.Models
{
    public class Company : Entity
    {

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; }
        public ICollection<Employee> Employees { get; set; } = [];
    }
}