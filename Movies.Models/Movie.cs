using System;

namespace Movies.Models
{
    /// <summary>
    /// Movie model class    
    /// </summary>
    public class Movie
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool Enabled { get; set; }
        public Gender Gender { get; set; }
        #endregion
    }
}
