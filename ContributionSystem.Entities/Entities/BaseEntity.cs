using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.Entities.Entities
{
    abstract public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
