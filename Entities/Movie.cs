using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies.Entities
{
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Range(1, 10)]
        [Required]
        public double Rating { get; set; }

        [Required]
        public DateTimeOffset RealiseDate { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Genre> Genres { get; set; }

        [Obsolete("Used only for Entities binding.", true)]
        public Movie() { }

        public Movie(string title, string description, double rating, DateTimeOffset realiseDate, ICollection<Actor> actors, ICollection<Genre> genres)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Rating = rating;
            RealiseDate = realiseDate;
            Actors = actors;
            Genres = genres;
        }
    }
}