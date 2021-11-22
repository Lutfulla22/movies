using System.ComponentModel.DataAnnotations;

namespace movies.Models
{
    public class NewGenre
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}