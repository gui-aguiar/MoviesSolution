using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public ICollection<Movie> MoviesList { get; set; }
        public string CustomerCPF { get; set; }
        public DateTime RentalDateTime { get; set; }
    }
}
