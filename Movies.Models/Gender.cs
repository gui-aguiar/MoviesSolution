using System;

namespace Movies.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool Enabled { get; set; }
    }
}