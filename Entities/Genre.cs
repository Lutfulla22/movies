using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace movies.Entities
{
    public class Genre
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Movie> Movies { get; set; }



        [Obsolete("Used only for Entities binding.", true)]
        public Genre() { }

        public Genre(string name, ICollection<Movie> movies = default)
        {
            Id = Guid.NewGuid();
            Name = name;
            Movies = movies;
        }
    }

}