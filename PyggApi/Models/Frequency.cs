using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class Frequency
    {
        [Required]
        public string FrequencyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FrequencyName { get; set; }

        public string FrequencyDescription { get; set; }
    }
}
