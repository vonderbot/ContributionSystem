using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.Entities.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
