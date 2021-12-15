using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.Entities.Entities
{
    /// <summary>
    /// Base class for all entities.
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
