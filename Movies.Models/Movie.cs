using System;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool Enabled { get; set; }
        public Gender Gender { get; set; }
    }
}
