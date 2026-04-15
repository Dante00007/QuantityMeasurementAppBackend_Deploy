using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuantityMeasurementAppModelLayer.Entity
{
    public class QuantityMeasurementHistoryEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public double InputValue1 { get; set; }

        [Required, StringLength(50)]
        public string InputUnit1 { get; set; }

        public double? InputValue2 { get; set; }
        public string? InputUnit2 { get; set; }

        [Required, StringLength(50)]
        public string TargetUnit { get; set; }

        [Required, StringLength(20)]
        public string Operation { get; set; }

        public double ResultValue { get; set; }

        [Required, StringLength(50)]
        public string ResultUnit { get; set; }
        [Required]
        public string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [JsonIgnore]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserEntity User { get; set; }

    }
}