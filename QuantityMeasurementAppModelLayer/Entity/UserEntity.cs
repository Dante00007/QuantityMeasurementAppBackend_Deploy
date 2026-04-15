using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuantityMeasurementAppModelLayer.Entity
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required,JsonIgnore]
        public string Password { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, Phone, StringLength(15)]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property: One user can have many history records
        public ICollection<QuantityMeasurementHistoryEntity> Histories { get; set; }

        public UserEntity()
        {
            Id = Guid.NewGuid(); // Set ID here
            Histories = new List<QuantityMeasurementHistoryEntity>(); // Initialize here
            CreatedAt = DateTime.Now;
        }
    }
}