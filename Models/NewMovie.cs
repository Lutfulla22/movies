using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movies.Models
{
    public class NewMovie
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Range(1, 10)]
        [Required]
        public double Rating { get; set; }

        [Required]
        public DateTimeOffset RealiseDate { get; set; }

        [Required]
        public IEnumerable<Guid> ActorsId { get; set; }

        [Required]
        public IEnumerable<Guid> GenresId { get; set; }
    }
}