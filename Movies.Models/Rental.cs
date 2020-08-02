using System;
using System.Collections.Generic;

namespace Movies.Models
{
    /// <summary>
    /// Rental model class    
    /// </summary>
    public class Rental
    {
        #region Properties
        public int Id { get; set; }
        public ICollection<Movie> MoviesList { get; set; }
        public string CustomerCPF { get; set; }
        public DateTime RentalDateTime { get; set; }
        #endregion
    }
}
