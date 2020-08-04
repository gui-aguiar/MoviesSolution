namespace Movies.Models
{
    /// <summary>
    /// API User model class    
    /// </summary>
    public class User
    {
        #region Properties
        public int Id { get; set; }
        public string Login { get; set; }        
        public string Password { get; set; }
        #endregion
    }
}
