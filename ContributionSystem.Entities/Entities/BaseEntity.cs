using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.Entities.Entities
{
    /// <summary>
    /// Primary key to entity.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
