using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace movies.Entities
{
    public class Actor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Fullname { get; set; }

        [Required]
        public DateTimeOffset Birthdate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Movie> Movies { get; set; }

        [Obsolete("Used only for Entities binding.", true)]
        public Actor() { }

        public Actor(string fullname, DateTimeOffset birthdate, ICollection<Movie> movies)
        {
            Id = Guid.NewGuid();
            Fullname = fullname;
            Birthdate = birthdate;
            Movies = movies;
        }
    }
}