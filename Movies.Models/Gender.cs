using System;

namespace Movies.Models
{    
    /// <summary>
    /// Gender model class    
    /// </summary>
    public class Gender
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool Enabled { get; set; }
        #endregion
    }
}